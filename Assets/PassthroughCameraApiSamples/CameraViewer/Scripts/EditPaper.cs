using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.Calib3dModule;

public class EditPaper : MonoBehaviour
{
    public FrameCapture frameCapture;
    public ImageProjector projector;
    public GameObject paperPlane;
    public Texture2D sourceOutlineTexture;
    public int projectedWidth = 1024;
    public int projectedHeight = 1024;

    private MeshRenderer planeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        planeRenderer = paperPlane.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Mat captured_frame = frameCapture.CaptureFrame();

        List<Point> aruco_points = SketchUtils.DetectMarkers(captured_frame);
        if (aruco_points.Count != 4) return;
        // These values are in meters
        var objPts = new MatOfPoint3f(
            new Point3(0.05f, 0.05f, 0), 
            new Point3(0.1659f, 0.05f, 0), 
            new Point3(0.1659f, 0.1659f, 0), 
            new Point3(0.05f, 0.1659f, 0)
        );
        var imgPts = new MatOfPoint2f(aruco_points.ToArray());

        Mat intrinsic_mat = frameCapture.GetIntrinsicMat();
        MatOfDouble distCoeffs = new MatOfDouble(new double[]{ 0, 0, 0, 0, 0 });  // TODO: Check for a dist_coeffs

        Mat rvec = new Mat(), tvec = new Mat();

        Calib3d.solvePnP(objPts, imgPts, intrinsic_mat, distCoeffs, rvec, tvec);

        // 1) Convert rvec → rotation matrix
        Mat rotMat = new Mat();
        Calib3d.Rodrigues(rvec, rotMat);

        // 2) Build a Unity Matrix4x4, inserting the CV rotMat
        var M = Matrix4x4.identity;
        M.m00 = (float)rotMat.get(0,0)[0];
        M.m01 = (float)rotMat.get(0,1)[0];
        M.m02 = (float)rotMat.get(0,2)[0];
        M.m10 = (float)rotMat.get(1,0)[0];
        M.m11 = (float)rotMat.get(1,1)[0];
        M.m12 = (float)rotMat.get(1,2)[0];
        M.m20 = (float)rotMat.get(2,0)[0];
        M.m21 = (float)rotMat.get(2,1)[0];
        M.m22 = (float)rotMat.get(2,2)[0];

        // 3) Adjust for coord‑system flip (Unity’s Y is up, OpenCV’s is down)
        var cvToUnity = Matrix4x4.Scale(new Vector3(1, -1, 1)); // TODO: Check these
        M = cvToUnity * M * cvToUnity;

        // 4) Extract Quaternion
        Quaternion poseRot = Quaternion.LookRotation(
            M.GetColumn(2),  // forward
            M.GetColumn(1)); // upward

        // 5) Build Unity position: flip Y from tvec
        Vector3 posePos = new Vector3(
            (float)tvec.get(0,0)[0],
        -(float)tvec.get(1,0)[0],
            (float)tvec.get(2,0)[0]
        );

        // 6) Apply to your plane
        paperPlane.transform.localPosition = posePos;
        paperPlane.transform.localRotation = poseRot;

        if (planeRenderer != null)
        {
            planeRenderer.material.mainTexture = sourceOutlineTexture;
        }



        // 2D-Implementation w/ Homography, try this if 3D method does not work
        // List<Point> image_points = new List<Point> {
        //     new Point(0, 0),         // Top-left
        //     new Point(1024, 0),      // Top-right
        //     new Point(1024, 1024),   // Bottom-right
        //     new Point(0, 1024)       // Bottom-left
        // };
        // Mat homography = SketchUtils.ComputeHomography(image_points, aruco_points);

        // Texture2D projectedTexture = projector.ProjectImage(homography);

        // if (planeRenderer != null)
        // {
        //     planeRenderer.material.mainTexture = projectedTexture;
        // }
    }
}
