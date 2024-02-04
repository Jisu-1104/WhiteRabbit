// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class HandTrackingSolution : ImageSourceSolution<HandTrackingGraph>
    {

        [SerializeField] private DetectionListAnnotationController _palmDetectionsAnnotationController;
        [SerializeField] private NormalizedRectListAnnotationController _handRectsFromPalmDetectionsAnnotationController;
        [SerializeField] private MultiHandLandmarkListAnnotationController _handLandmarksAnnotationController;
        [SerializeField] private NormalizedRectListAnnotationController _handRectsFromLandmarksAnnotationController;

        public List<string[]> data = new List<string[]>();
        public HandTrackingGraph.ModelComplexity modelComplexity
        {
            get => graphRunner.modelComplexity;
            set => graphRunner.modelComplexity = value;
        }

        public int maxNumHands
        {
            get => graphRunner.maxNumHands;
            set => graphRunner.maxNumHands = value;
        }

        public float minDetectionConfidence
        {
            get => graphRunner.minDetectionConfidence;
            set => graphRunner.minDetectionConfidence = value;
        }

        public float minTrackingConfidence
        {
            get => graphRunner.minTrackingConfidence;
            set => graphRunner.minTrackingConfidence = value;
        }

        protected override void OnStartRun()
        {

            if (!runningMode.IsSynchronous())
            {
                graphRunner.OnPalmDetectectionsOutput += OnPalmDetectionsOutput;
                graphRunner.OnHandRectsFromPalmDetectionsOutput += OnHandRectsFromPalmDetectionsOutput;
                graphRunner.OnHandLandmarksOutput += OnHandLandmarksOutput;
                // TODO: render HandWorldLandmarks annotations
                graphRunner.OnHandRectsFromLandmarksOutput += OnHandRectsFromLandmarksOutput;
                graphRunner.OnHandednessOutput += OnHandednessOutput;
            }

            var imageSource = ImageSourceProvider.ImageSource;
            SetupAnnotationController(_palmDetectionsAnnotationController, imageSource, true);
            SetupAnnotationController(_handRectsFromPalmDetectionsAnnotationController, imageSource, true);
            SetupAnnotationController(_handLandmarksAnnotationController, imageSource, true);
            SetupAnnotationController(_handRectsFromLandmarksAnnotationController, imageSource, true);
        }

        protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
        {
            graphRunner.AddTextureFrameToInputStream(textureFrame);
        }
        protected override IEnumerator WaitForNextValue()
        {
            List<Detection> palmDetections = null;
            List<NormalizedRect> handRectsFromPalmDetections = null;
            List<NormalizedLandmarkList> handLandmarks = null;
            List<LandmarkList> handWorldLandmarks = null;
            List<NormalizedRect> handRectsFromLandmarks = null;
            List<ClassificationList> handedness = null;


            if (runningMode == RunningMode.Sync)
            {
                var _ = graphRunner.TryGetNext(out palmDetections, out handRectsFromPalmDetections, out handLandmarks, out handWorldLandmarks, out handRectsFromLandmarks, out handedness, true);

                if (Input.GetKey(KeyCode.Escape))
                {
                    string[] lmDataX = new string[21];
                    string[] lmDataY = new string[21];
                    string[] lmDataZ = new string[21];
                    if (handLandmarks != null && handLandmarks.Count > 0)
                    {
                        data.Clear();
                        for (int i = 0; i < 21; i++)
                        {
                            foreach (var landmarks in handLandmarks)
                            {
                                // top of the head
                                var landmarkposition = landmarks.Landmark[i];
                                lmDataX[i] = landmarkposition.X.ToString();
                                lmDataY[i] = landmarkposition.Y.ToString();
                                lmDataZ[i] = landmarkposition.Z.ToString();

                                //Debug.Log($"{i} : {lmdDataX[i]} , {lmdDataY[i]} , {lmdDataZ[i]}");
                            }
                        }
                        data.Add(lmDataX);
                        data.Add(lmDataY);
                        data.Add(lmDataZ);

                        Debug.Log("line: " + GetLmDataString());
                    }
                }

            }

            else if (runningMode == RunningMode.NonBlockingSync)
            {
                yield return new WaitUntil(() => graphRunner.TryGetNext(out palmDetections, out handRectsFromPalmDetections, out handLandmarks, out handWorldLandmarks, out handRectsFromLandmarks, out handedness, false));
            }




            _palmDetectionsAnnotationController.DrawNow(palmDetections);
            _handRectsFromPalmDetectionsAnnotationController.DrawNow(handRectsFromPalmDetections);
            _handLandmarksAnnotationController.DrawNow(handLandmarks, handedness);
            // TODO: render HandWorldLandmarks annotations
            _handRectsFromLandmarksAnnotationController.DrawNow(handRectsFromLandmarks);
        }
        public string GetLmDataString()
        {
            // data 리스트가 비어있는 경우 빈 문자열 반환
            if (data.Count == 0)
            {
                return "";
            }

            // 리스트의 배열들을 합쳐서 문자열로 변환하여 반환
            return string.Join(",", data.SelectMany(arr => arr));
        }

        private void OnPalmDetectionsOutput(object stream, OutputEventArgs<List<Detection>> eventArgs)
        {
            _palmDetectionsAnnotationController.DrawLater(eventArgs.value);
        }

        private void OnHandRectsFromPalmDetectionsOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
        {
            _handRectsFromPalmDetectionsAnnotationController.DrawLater(eventArgs.value);
        }

        private void OnHandLandmarksOutput(object stream, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
        {
            _handLandmarksAnnotationController.DrawLater(eventArgs.value);
        }

        private void OnHandRectsFromLandmarksOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
        {
            _handRectsFromLandmarksAnnotationController.DrawLater(eventArgs.value);
        }

        private void OnHandednessOutput(object stream, OutputEventArgs<List<ClassificationList>> eventArgs)
        {
            _handLandmarksAnnotationController.DrawLater(eventArgs.value);
        }


    }
}
