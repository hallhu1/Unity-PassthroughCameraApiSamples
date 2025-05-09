
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.MlModule
{

    // C++: class RTrees
    /// <summary>
    ///  The class implements the random forest predictor.
    /// </summary>
    /// <remarks>
    ///  @sa @ref ml_intro_rtrees
    /// </remarks>
    public partial class RTrees : DTrees
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        ml_RTrees_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal RTrees(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new RTrees __fromPtr__(IntPtr addr) { return new RTrees(addr); }

        //
        // C++:  bool cv::ml::RTrees::getCalculateVarImportance()
        //

        /// <remarks>
        ///  @see setCalculateVarImportance
        /// </remarks>
        public bool getCalculateVarImportance()
        {
            ThrowIfDisposed();

            return ml_RTrees_getCalculateVarImportance_10(nativeObj);


        }


        //
        // C++:  void cv::ml::RTrees::setCalculateVarImportance(bool val)
        //

        /// <remarks>
        ///  @copybrief getCalculateVarImportance @see getCalculateVarImportance
        /// </remarks>
        public void setCalculateVarImportance(bool val)
        {
            ThrowIfDisposed();

            ml_RTrees_setCalculateVarImportance_10(nativeObj, val);


        }


        //
        // C++:  int cv::ml::RTrees::getActiveVarCount()
        //

        /// <remarks>
        ///  @see setActiveVarCount
        /// </remarks>
        public int getActiveVarCount()
        {
            ThrowIfDisposed();

            return ml_RTrees_getActiveVarCount_10(nativeObj);


        }


        //
        // C++:  void cv::ml::RTrees::setActiveVarCount(int val)
        //

        /// <remarks>
        ///  @copybrief getActiveVarCount @see getActiveVarCount
        /// </remarks>
        public void setActiveVarCount(int val)
        {
            ThrowIfDisposed();

            ml_RTrees_setActiveVarCount_10(nativeObj, val);


        }


        //
        // C++:  TermCriteria cv::ml::RTrees::getTermCriteria()
        //

        /// <remarks>
        ///  @see setTermCriteria
        /// </remarks>
        public TermCriteria getTermCriteria()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[3];
            ml_RTrees_getTermCriteria_10(nativeObj, tmpArray);
            TermCriteria retVal = new TermCriteria(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::ml::RTrees::setTermCriteria(TermCriteria val)
        //

        /// <remarks>
        ///  @copybrief getTermCriteria @see getTermCriteria
        /// </remarks>
        public void setTermCriteria(TermCriteria val)
        {
            ThrowIfDisposed();

            ml_RTrees_setTermCriteria_10(nativeObj, val.type, val.maxCount, val.epsilon);


        }


        //
        // C++:  Mat cv::ml::RTrees::getVarImportance()
        //

        /// <remarks>
        ///  Returns the variable importance array.
        ///      The method returns the variable importance vector, computed at the training stage when
        ///      CalculateVarImportance is set to true. If this flag was set to false, the empty matrix is
        ///      returned.
        /// </remarks>
        public Mat getVarImportance()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(ml_RTrees_getVarImportance_10(nativeObj)));


        }


        //
        // C++:  void cv::ml::RTrees::getVotes(Mat samples, Mat& results, int flags)
        //

        /// <remarks>
        ///  Returns the result of each individual tree in the forest.
        ///      In case the model is a regression problem, the method will return each of the trees'
        ///      results for each of the sample cases. If the model is a classifier, it will return
        ///      a Mat with samples + 1 rows, where the first row gives the class number and the
        ///      following rows return the votes each class had for each sample.
        /// </remarks>
        /// <param name="samples">
        /// Array containing the samples for which votes will be calculated.
        /// </param>
        /// <param name="results">
        /// Array where the result of the calculation will be written.
        /// </param>
        /// <param name="flags">
        /// Flags for defining the type of RTrees.
        /// </param>
        public void getVotes(Mat samples, Mat results, int flags)
        {
            ThrowIfDisposed();
            if (samples != null) samples.ThrowIfDisposed();
            if (results != null) results.ThrowIfDisposed();

            ml_RTrees_getVotes_10(nativeObj, samples.nativeObj, results.nativeObj, flags);


        }


        //
        // C++:  double cv::ml::RTrees::getOOBError()
        //

        /// <remarks>
        ///  Returns the OOB error value, computed at the training stage when calcOOBError is set to true.
        ///         If this flag was set to false, 0 is returned. The OOB error is also scaled by sample weighting.
        /// </remarks>
        public double getOOBError()
        {
            ThrowIfDisposed();

            return ml_RTrees_getOOBError_10(nativeObj);


        }


        //
        // C++: static Ptr_RTrees cv::ml::RTrees::create()
        //

        /// <remarks>
        ///  Creates the empty model.
        ///      Use StatModel::train to train the model, StatModel::train to create and train the model,
        ///      Algorithm::load to load the pre-trained model.
        /// </remarks>
        public static new RTrees create()
        {


            return RTrees.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_RTrees_create_10()));


        }


        //
        // C++: static Ptr_RTrees cv::ml::RTrees::load(String filepath, String nodeName = String())
        //

        /// <summary>
        ///  Loads and creates a serialized RTree from a file
        /// </summary>
        /// <remarks>
        ///         Use RTree::save to serialize and store an RTree to disk.
        ///         Load the RTree from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized RTree
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static new RTrees load(string filepath, string nodeName)
        {


            return RTrees.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_RTrees_load_10(filepath, nodeName)));


        }

        /// <summary>
        ///  Loads and creates a serialized RTree from a file
        /// </summary>
        /// <remarks>
        ///         Use RTree::save to serialize and store an RTree to disk.
        ///         Load the RTree from this file again, by calling this function with the path to the file.
        ///         Optionally specify the node for the file containing the classifier
        /// </remarks>
        /// <param name="filepath">
        /// path to serialized RTree
        /// </param>
        /// <param name="nodeName">
        /// name of node containing the classifier
        /// </param>
        public static new RTrees load(string filepath)
        {


            return RTrees.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(ml_RTrees_load_11(filepath)));


        }


#if (UNITY_IOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  bool cv::ml::RTrees::getCalculateVarImportance()
        [DllImport(LIBNAME)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool ml_RTrees_getCalculateVarImportance_10(IntPtr nativeObj);

        // C++:  void cv::ml::RTrees::setCalculateVarImportance(bool val)
        [DllImport(LIBNAME)]
        private static extern void ml_RTrees_setCalculateVarImportance_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool val);

        // C++:  int cv::ml::RTrees::getActiveVarCount()
        [DllImport(LIBNAME)]
        private static extern int ml_RTrees_getActiveVarCount_10(IntPtr nativeObj);

        // C++:  void cv::ml::RTrees::setActiveVarCount(int val)
        [DllImport(LIBNAME)]
        private static extern void ml_RTrees_setActiveVarCount_10(IntPtr nativeObj, int val);

        // C++:  TermCriteria cv::ml::RTrees::getTermCriteria()
        [DllImport(LIBNAME)]
        private static extern void ml_RTrees_getTermCriteria_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::ml::RTrees::setTermCriteria(TermCriteria val)
        [DllImport(LIBNAME)]
        private static extern void ml_RTrees_setTermCriteria_10(IntPtr nativeObj, int val_type, int val_maxCount, double val_epsilon);

        // C++:  Mat cv::ml::RTrees::getVarImportance()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_RTrees_getVarImportance_10(IntPtr nativeObj);

        // C++:  void cv::ml::RTrees::getVotes(Mat samples, Mat& results, int flags)
        [DllImport(LIBNAME)]
        private static extern void ml_RTrees_getVotes_10(IntPtr nativeObj, IntPtr samples_nativeObj, IntPtr results_nativeObj, int flags);

        // C++:  double cv::ml::RTrees::getOOBError()
        [DllImport(LIBNAME)]
        private static extern double ml_RTrees_getOOBError_10(IntPtr nativeObj);

        // C++: static Ptr_RTrees cv::ml::RTrees::create()
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_RTrees_create_10();

        // C++: static Ptr_RTrees cv::ml::RTrees::load(String filepath, String nodeName = String())
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_RTrees_load_10(string filepath, string nodeName);
        [DllImport(LIBNAME)]
        private static extern IntPtr ml_RTrees_load_11(string filepath);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ml_RTrees_delete(IntPtr nativeObj);

    }
}
