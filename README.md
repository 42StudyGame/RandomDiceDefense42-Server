# RandomDiceDefense42-Server
42랜덤다이스 서버 레포입니다.

# 외부 종속성 관리
## 1. 라이브러리 파일 추가 방식 약속
`/프로젝트루트/Libraries/라이브러리명/Debug/\*\*\*.lib`<br>
`/프로젝트루트/Libraries/라이브러리명/Debug/\*\*\*.dll`<br>
`/프로젝트루트/Libraries/라이브러리명/Debug/\*\*\*.h`<br>
라이브러리 특성에 맞게 Debug or Release 선택.

## 2. 외부 소스파일들 추가 방식 약속
`/프로젝트루트/OtherSrcs/해당소스코드폴드`

## 3. 의존성 경로 추가방법
### 3.1. VS환경변수([링크](https://learn.microsoft.com/ko-kr/cpp/build/reference/common-macros-for-build-commands-and-properties?view=msvc-170))

- `$(ProjectDir)` : 프로젝트의 디렉터리(드라이브 + 경로로 정의됨)이며, 뒤의 백슬래시 '\'를 포함해야합니다.<br>
	-> `.vcxproj`이 있는 경로. 예시 : `$(ProjectDir)MatchServer\x64\Debug`
- `$(SolutionDir)` : 솔루션의 디렉터리(드라이브 + 경로로 정의됨)이며, 뒤의 백슬래시 '\'를 포함해야합니다. IDE에서 솔루션을 빌드할 때만 정의됩니다.<br>
	-> `.sln`이 있는 경로. 예시 : `$(SolutionDir)Libraries\redis-3.0\Debug`
- `$(OutDir)` : 출력 파일 디렉터리에 대한 경로입니다.
### 3.2. 라이브러리 의존성 경로 추가 방법
#### 3.2.1 라이브러리 경로 추가 설정
🧭\[at\] VS의 프로젝트 - 링커 - 일반 - 추가 라이브러리 디렉터리

📦\[what\]	라이브러리가 있는 디렉토리를 추가.

#### 3.2.2 라이브러리 파일명 추가 설정
🧭\[at\] VS의 프로젝트 - 링커 - 입력 - 추가종속성

📦\[what\] 라이브러리 파일(`.lib`, `.dll`)파일 추가

#### 3.2.3 헤더 파일위치 추가 설정
🧭\[at\] VS의 프로젝트 - 속성 - C/C++ - 추가 포함 디렉토리

📦\[what\] 헤더가 있는 디렉토리

🧭\[at\] VS의 프로젝트 - 구성속성 - VC++디렉토리 - 참조 디렉토리

📦\[what\] 헤더가 있는 디렉토리