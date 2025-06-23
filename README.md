🚀 DroneProject HTTP 통신 서버 실행 방법
이 문서는 DroneProject의 HTTP 통신 서버를 실행하는 방법을 안내합니다.

💻 서버 실행 단계
Python 기반 웹 서버를 실행하기 위한 단계별 지침입니다.

PowerShell (관리자 권한) 실행

PowerShell을 관리자 권한으로 실행합니다.

실행 정책 설정

다음 명령어를 입력하고 Enter를 누릅니다.

Set-ExecutionPolicy RemoteSigned -Scope CurrentUser

(필요시) 변경 사항 적용을 위해 Y를 입력합니다.

PowerShell 일반 실행

관리자 권한이 아닌, 일반 PowerShell 창을 새로 엽니다.

프로젝트 디렉토리로 이동

다음 명령어를 입력하여 로컬 저장소 내의 PythonWebServer 디렉토리로 이동합니다.

cd C:\(본인 깃 파일 로컬 저장소 주소 입력)\PythonWebServer

예시: cd C:\DroneControlProject\PythonWebServer

가상 환경 활성화

PythonWebServer 디렉토리에서 다음 명령어를 입력하고 Enter를 누릅니다.

.\venv\Scripts\activate

성공적으로 활성화되면 프롬프트 맨 앞에 (venv)가 표시됩니다.
예시: (venv) PS C:\DroneControlProject\PythonWebServer>

서버 애플리케이션 실행

가상 환경이 활성화된 상태에서 다음 명령어를 입력하고 Enter를 누릅니다.

app.py

서버 실행 확인

터미널 창에 http://127.0.0.1:5000과 같은 메시지가 나타나고, 오류 없이 서버가 실행 중인지 확인합니다.

🌐 서버 접속 방법
실행된 HTTP 서버에 접속하는 방법입니다.

본인 PC에서 접속:

웹 브라우저에 http://127.0.0.1:(포트번호)를 입력하여 접속할 수 있습니다.

예시: http://127.0.0.1:5000

다른 PC에서 접속:

다른 PC에서 접속하려면 http://(본인 IP 주소):(포트번호) 형식으로 주소를 입력해야 합니다.

예시: http://192.168.0.10:5000 (본인 PC의 IP 주소에 따라 변경)

📚 정리 자료 참고
자세한 내용 및 관련 자료는 다음 링크에서 확인하실 수 있습니다.

Unity Python 연동 관련 정리 자료