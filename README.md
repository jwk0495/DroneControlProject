DroneProject 폴더 구성
<details>
<summary><b>폴더 구조 보기</b></summary>

PythonWebServer: HTTP 웹 서버 전용 폴더

UnitySimulation: 유니티 프로젝트 파일 관련 폴더

</details>

DroneProject HTTP 통신 서버 실행 방법
다음 단계에 따라 HTTP 통신 서버를 실행할 수 있습니다.

PowerShell 관리자 권한으로 실행:

시작 메뉴에서 "PowerShell"을 검색하여 마우스 오른쪽 버튼을 클릭하고 "관리자 권한으로 실행"을 선택합니다.

실행 정책 설정:

PowerShell 창에 다음 명령어를 입력하고 Enter 키를 누릅니다.

Set-ExecutionPolicy RemoteSigned -Scope CurrentUser

프롬프트가 나타나면 Y를 입력하고 Enter 키를 눌러 변경을 확인합니다.

일반 PowerShell 실행:

새로운 PowerShell 창을 관리자 권한 없이 일반 사용자로 실행합니다.

PythonWebServer 디렉토리로 이동:

cd 명령어를 사용하여 Python 웹 서버 폴더로 이동합니다.

cd C:\(본인 깃 파일 로컬 저장소 주소 입력)\PythonWebServer

(예시: cd C:\Users\YourUser\Documents\GitHub\DroneProject\PythonWebServer)

가상 환경 활성화:

다음 명령어를 입력하고 Enter 키를 누릅니다.

.\venv\Scripts\activate

성공적으로 활성화되면 프롬프트 맨 앞에 (venv)가 표시됩니다.
(예시: (venv) PS C:\DroneControlProject\PythonWebServer>)

HTTP 서버 실행:

가상 환경이 실행된 상태에서 app.py를 입력하고 Enter 키를 누릅니다.

app.py

터미널 창에 "http://127.0.0.1:5000"과 같은 메시지가 나타나고 오류 없이 실행 중인지 확인합니다.

서버 접속:

본인 PC에서 접속: 웹 브라우저에 http://127.0.0.1:(포트번호)를 입력하여 접속합니다. (기본 포트 5000)

다른 PC에서 접속: http://(본인 IP 주소):(포트번호) 형식으로 주소를 입력해야 연결됩니다. (예시: http://192.168.1.100:5000)

<details>
<summary><b>🚀 깃 참고 자료</b></summary>

Unity-Python 연동 가이드 (Notion)

</details>