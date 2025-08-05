# Archero\_Project

## 🎮 게임 소개
**『현대세계 최후의 궁수』**는 모바일 게임 궁수의 전설을 모티브로 한 2.5D 원거리 액션 게임입니다.
플레이어는 다른 세계에서 깨어난 궁수가 되어, 현대 도시 속에서 사라진 동료를 찾아 다양한 스테이지를 돌파해 나가야 합니다.

Unity 엔진 기반으로 개발되었으며, FSM, ScriptableObject, DoTween, Generic Singleton 상태 전환 시스템 등 다양한 기술을 활용하여 몰입감 있는 전투 흐름과 탄탄한 구조를 구현하였습니다.

- 장르: 2.5D 원거리 액션 게임

- 플랫폼: PC (Windows)

- 개발 툴: Unity 2022.3.17f1, Visual Studio, GitHub

- 개발 기간: 2025.07.29 ~ 2025.08.05 (총 1주)


## 🕹️ 플레이 방법

이동: WASD 키 또는 방향키

마우스 방향으로 자동 조준 및 공격

- 스테이지에 진입하면 캐릭터는 마우스 방향을 바라보고 자동으로 공격을 시작합니다.

- 공격: 자동 발사 (플레이어는 방향만 조절)

- 스킬 선택: 방 클리어 후 무작위 3개의 스킬 중 하나를 선택

  게임 흐름

- 메인 화면에서 게임 시작

- 무작위 방 클리어 → 다음 방 입장

- 각 방 클리어 시 스킬 선택

- 최종적으로 보스 방 클리어 시 승리

게임 오버

- 체력이 0이 되면 게임 종료 후 ‘Game Over’ UI 출력

- ‘재시작’ 버튼 클릭 시 처음부터 다시 시작

## 🛠 사용 기술
- 🎮 Unity 2022.3 LTS
- 🧠 C# (게임 로직 및 시스템 구현)
- 🗂 Git & GitHub (버전 관리 및 협업)
- 🎨 Figma, Aseprite (UI 디자인 및 도트 리소스 제작)
- 🛠 TextMeshPro, DoTween (UI 시스템 구성 및 애니메이션)
- 📦 ScriptableObject (스킬 및 데이터 관리)
- 🎛 FSM (상태 전이 기반 보스 AI 및 몬스터 행동)
- 🧠 Generic Singleton (매니저 관리)


## ✨ 주요 구현 기능
🧩 랜덤 방 생성 시스템
- 스테이지마다 랜덤 구조의 방이 생성되어 플레이어의 탐험 재미 강화

🧠 스킬 선택 & 업그레이드 시스템
- 방 클리어 시 무작위 3개 스킬 중 선택
- ScriptableObject로 관리하여 확장 용이

🧟 몬스터 및 보스 AI
- 근거리/원거리 몬스터의 패턴 구성
- 보스는 FSM 구조를 통해 상태 전이 기반 공격 패턴 구현

🎮 전투 시스템
- 플레이어 이동, 조준, 공격 및 피격 반응 구현
- 몬스터 사망 시 투사체 충돌 무효화 처리

🖼️ UI 시스템
- 게임 시작/오버/스킬 선택/설정 등 상황별 UI 구성
- DoTween 기반 자연스러운 UI 전환

🔊 사운드 시스템
- 효과음, 배경음, 보스 등장음 등 상황별 사운드 매니저로 제어

📦 최적화
- 제너릭 싱글톤, 오브젝트 풀링, 몬스터 중복 스폰 방지 처리


## 🖼️ 게임 화면
아래는 실제 게임 플레이 화면입니다.  
![Animation](https://github.com/user-attachments/assets/51afc725-65f0-4318-b454-c70592ed5fd6)  


## 📂 프로젝트 폴더 구조  
  
📦 02. Scripts/  
├── 📂Boss/                        # 보스 관련 제어 스크립트  
│   ├── Axe.cs                    # 보스 무기 충돌 감지  
│   ├── BossController.cs        # 보스 상태 전이 및 FSM  
│   └── BossHpBar.cs             # 보스 체력 UI  
├── 📂Camera/  
│   └── CanvasCameraSetter.cs    # UI 캔버스에 메인 카메라 자동 연결  
├── 📂Manager/                    # 전역 시스템 관리자 클래스들  
│   ├── 📂Camera/  
│   │   └── CameraManager.cs  
│   ├── 📂Monster/  
│   │   └── MonsterSpawnManager.cs  
│   ├── 📂PlayerSpawn/  
│   │   └── PlayerSpawnManager.cs  
│   ├── 📂Skill/  
│   │   └── SkillManager.cs    
│   ├── 📂Sound/  
│   │   ├── PlayerSound.cs  
│   │   └── SoundManager.cs  
│   ├── 📂Stage/  
│   │   ├── NextStageParticle.cs  
│   │   └── StageManager.cs  
│   ├── 📂Tutorial/  
│   │   └── SkillTutorialUIManager.cs    
│   ├── 📂UI/  
│   │   ├── 📂Player/  
│   │   │   ├── PlayerHpBar.cs  
│   │   │   └── PlayerStatUI.cs  
│   │   ├── 📂SettingUI/  
│   │   │   └── SoundUI.cs  
│   │   ├── 📂Skill/  
│   │   │   └── SkillUIManager.cs  
│   │   ├── 📂Tutorial/    
│   │   │   └── TutorialSlotUI.cs  
│   │   └── UIManager.cs  
│   └── GameManager.cs           # 게임 전반 흐름 관리    
├── 📂Monster/                    # 일반 몬스터 AI 및 전투 관련 스크립트    
│   ├── Melee_Damage.cs  
│   ├── MonsterFSM.cs      
│   ├── MonsterHpBar.cs    
│   ├── MonsterParticleControl.cs    
│   ├── MonsterProjectile.cs    
│   └── MonsterStat.cs  
├── 📂Player/                     # 플레이어 조작 및 상태 관리    
│   ├── Bullet.cs    
│   ├── PlayerMovement.cs  
│   ├── PlayerShooting.cs  
│   └── PlayerStat.cs    
├── 📂ScriptableObject/           # 스킬 데이터 저장용 ScriptableObject  
│   └── SkillData_ScriptableObject.cs    
├── 📂Singleton/                  # 제너릭 싱글톤 베이스 클래스    
│   └── Singleton.cs    
├── 📂Sound/                      # 사운드 재생 소스      
│   └── SoundSource.cs  
├── 📂Stage/                      # 스테이지 전환 로직    
│   └── StageChanger.cs  
├── 📂Test/                       # 테스트 및 디버깅용  
│   ├── End.cs    
│   └── TimeController.cs    
├── 📂UI/                         # UI 관련 스크립트  
│   ├── 📂Animation/    
│   │   └── UIAnimationHandler.cs    
│   ├── 📂Production/  
│   │   └── Siren.cs    
│   ├── 📂Skill/  
│   │   └── SkillSlotUI.cs  
│   └── 📂Title/      
│       ├── StageChangeButton.cs    
│       └── StateChangeButton.cs    
├── 📂Utils/                      # 유틸리티 스크립트  
└── └── EnemyUtil.cs  
  
## 👤 개발자
팀장 : 이재은 - 스킬 및 업그레이드 시스템, 전체 UI 구성, 프로젝트 전반 총괄   
팀원 : 김용민 - 랜덤 방 생성 시스템, 사운드 시스템 개발   
팀원 : 정세윤 - 보스 AI 및 전투 패턴 구현, 보스전 연출   
팀원 : 김유경 - 일반 적 AI 및 공격 패턴 설계  
팀원 : 김노아 - 플레이어 이동 및 공격 구현, QA 및 테스팅 담당  

## 🧠트러블 슈팅  

<img width="1100" height="630" alt="image" src="https://github.com/user-attachments/assets/84938ea9-d81c-47b7-ba77-d041f6209d8d" />  

<img width="1100" height="630" alt="image" src="https://github.com/user-attachments/assets/15fc5eec-df98-4703-9b26-8564c9368e53" />  

<img width="1100" height="630" alt="image" src="https://github.com/user-attachments/assets/e5a0f1a3-8f2c-487e-bc7e-7cd990c56d21" />  

<img width="1100" height="630" alt="image" src="https://github.com/user-attachments/assets/e4ad1a78-2004-4ef4-afad-db4d390c5813" />  

<img width="1100" height="630" alt="image" src="https://github.com/user-attachments/assets/ae0b236e-d576-40a1-8d42-e250e92a4e58" />  

<img width="1100" height="630" alt="image" src="https://github.com/user-attachments/assets/da8ed9a0-babe-4522-9df5-1ddb8882586e" />  



