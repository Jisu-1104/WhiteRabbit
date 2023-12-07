// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class HandTrackingSolution : ImageSourceSolution<HandTrackingGraph>
    {

        [SerializeField] private DetectionListAnnotationController _palmDetectionsAnnotationController;
        [SerializeField] private NormalizedRectListAnnotationController _handRectsFromPalmDetectionsAnnotationController;
        [SerializeField] private MultiHandLandmarkListAnnotationController _handLandmarksAnnotationController;
        [SerializeField] private NormalizedRectListAnnotationController _handRectsFromLandmarksAnnotationController;

        private List<string[]> data = new List<string[]>();
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
        void SaveCSV(string oldfileName)
        {
            string newFileName = oldfileName;
            //string[] allFiles = AssetDatabase.GetAllAssetPaths();
            //HashSet<string> uniqueNames = new HashSet<string>();

            //foreach (string oldfilePath in allFiles)
            //{
            //    string fileName = Path.GetFileNameWithoutExtension(oldfilePath);
            //    uniqueNames.Add(fileName);
            //}
            //if (!uniqueNames.Add(oldfileName))
            //{
            //    // 파일명이 중복되면 새로운 이름 생성
            //    int count = 1;
            //    newFileName = oldfileName;

            //    while (!uniqueNames.Add(newFileName))
            //    {
            //        newFileName = $"{oldfileName}_{count}";
            //        count++;
            //    }

            //}

            //AssetDatabase.Refresh();


            // 파일 경로 설정
            string filePath = Path.Combine(Application.dataPath, newFileName +".csv");

            // CSV 파일에 데이터 쓰기
            StreamWriter outStream = System.IO.File.CreateText(filePath);

            for (int i = 0; i < data.Count; i++)
            {
                string line = string.Join(",", data[i]);
                outStream.WriteLine(line);
            }

            outStream.Close();
            Debug.Log("CSV 파일이 성공적으로 저장되었습니다.");
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
                    string[] lmdDataX = new string[21];
                    string[] lmdDataY = new string[21];
                    string[] lmdDataZ = new string[21];
                    if (handLandmarks != null && handLandmarks.Count > 0)
                    {
                        for (int i = 0; i < 21; i++)
                        {
                            foreach (var landmarks in handLandmarks)
                            {
                                // top of the head
                                var landmarkposition = landmarks.Landmark[i];
                                lmdDataX[i] = landmarkposition.X.ToString();
                                lmdDataY[i] = landmarkposition.Y.ToString();
                                lmdDataZ[i] = landmarkposition.Z.ToString();

                                Debug.Log($"{i} : {lmdDataX[i]} , {lmdDataY[i]} , {lmdDataZ[i]}");
                            }
                        }
                        data.Add(lmdDataX);
                        data.Add(lmdDataY);
                        data.Add(lmdDataZ);
                    }
                                 
                    SaveCSV("example");
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
