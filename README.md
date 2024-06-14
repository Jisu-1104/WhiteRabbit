# Used open source
MediapipeUnityPlugin <https://github.com/homuler/MediaPipeUnityPlugin/wiki/Installation-Guide>

2D Platformer by Artyom Zagorskiy <https://assetstore.unity.com/packages/tools/game-toolkits/2d-platformer-229878>

Dynamic Space Background Lite by DinV Studio <https://assetstore.unity.com/packages/2d/textures-materials/dynamic-space-background-lite-104606>

2D Cartoon Jungle Pack by Marta Maksymiec <https://assetstore.unity.com/packages/2d/environments/2d-cartoon-jungle-pack-103170>

탑다운 2D RPG 에셋 팩 by Goldmetal <https://assetstore.unity.com/packages/2d/characters/top-down-2d-rpg-assets-pack-188718>

언데드 서바이버 에셋 팩 by Golemetal <https://assetstore.unity.com/packages/2d/undead-survivor-assets-pack-238068>


# How to Install (Window only)

## 설치 조건

python 3.9 이상

Bazelisk <- Bazel이면 안 된다. tools를 찾지 못한다는 에러메세지가 뜨는데, tools 파일은 bazel만 설치할 때는 다운로드 되지 않는다.

NuGet : version 상관 없음

Opencv 3.4.16

Visual Studio 2019 + Build Tools <https://visualstudio.microsoft.com/ko/visual-cpp-build-tools/>

Visual Studio Install에서 C++을 사용한 데스크톱 개발, SDK 추가 설치

## 설치 방법

0. Python, Bazelisk, Nuget을 환경 변수에 추가한다.

    Python을 설치할 때 path 추가를 눌러주면 편하다. Bazelisk를 설치할 때 또한, choco를 사용하면 자동으로 path를 추가해주어 편하다.
   

2. MSYS2를 설치한다. https://www.msys2.org/

    아마 C:\msys64에 설치되었을 것이다. 환경변수에 C:\msys64\usr\bin 을 추가한다.

    msys2를 실행하여 다음 코드를 입력한다.

    pacman -S git patch unzip
 

2. cmd를 켜 다음 코드를 입력해 Bazel 변수를 설정한다.

set BAZEL_VS=C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools

set BAZEL_VC=C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\VC

set BAZEL_VC_FULL_VERSION=(Your local VC version)

set BAZEL_WINSDK_FULL_VERSION=(Your local WinSDK version)

VS를 깔며 특별히 경로를 건들지 않은 이상 VS, VC는 그대로 붙여넣으면 될 것이다.

VC FULL VERSION은 C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\VC\Tools\MSVC 안에 있다. VS의 버젼에 따라 2019 다음 디렉토리의 이름은 Professional일 수도, Community일 수도 있다.

WINSDK_FULL_VERSION은 Visual Studio Installer에서 아까 설치한 SDK의 괄호 안 숫자를 적는다.

3. cmd에 다음 코드를 입력한다.

pip install numpy

4. <https://github.com/homuler/MediaPipeUnityPlugin>로 이동하여 code를 컴퓨터 안에 저장해준다.

5. code를 저장한 위치로 이동하여 다음 코드를 입력한다.

python build.py build --desktop cpu --opencv=cmake -v

이동하는 방법 :: 1) 저장한 위치를 파일 탐색기를 통해 열어 주소를 복사한다. 2) cmd에 cd 복사한 주소 입력한다.


# How to build

Unity 2022.03.13f1 을 설치하고 우리의 git repository를 저장하여 실행한다. 

# How to test

웹캠이 동작하는지 확인하고, ad로 캐릭터 이동, space bar로 점프하여 조작한다. 
