# Archero\_Project

## 🎮 게임 소개
**『현대세계 최후의 궁수』**는 모바일 게임 궁수의 전설을 모티브로 한 2.5D 원거리 액션 게임입니다.
플레이어는 다른 세계에서 깨어난 궁수가 되어, 현대 도시 속에서 사라진 동료를 찾아 다양한 스테이지를 돌파해 나가야 합니다.

Unity 엔진 기반으로 개발되었으며, FSM, ScriptableObject, DoTween, 상태 전환 시스템 등 다양한 기술을 활용하여 몰입감 있는 전투 흐름과 탄탄한 구조를 구현하였습니다.

- 장르: 2.5D 원거리 액션 게임

- 플랫폼: PC (Windows)

- 개발 툴: Unity 2022.3.17f1, Visual Studio, GitHub

- 개발 기간: 2025.07.29 ~ 2025.08.05 (총 1주)


## 🕹️ 플레이 방법
- 이동: WASD 키 또는 방향키

- 마우스 방향으로 자동 조준 및 공격

- 스테이지에 진입하면 캐릭터는 마우스 방향을 바라보고 자동으로 공격을 시작합니다.

- 공격: 자동 발사 (플레이어는 방향만 조절)

- 스킬 선택: 방 클리어 후 무작위 3개의 스킬 중 하나를 선택

- 게임 흐름

- 메인 화면에서 게임 시작

- 무작위 방 클리어 → 다음 방 입장

- 각 방 클리어 시 스킬 선택

- 최종적으로 보스 방 클리어 시 승리

- 게임 오버

- 체력이 0이 되면 게임 종료 후 ‘Game Over’ UI 출력

- ‘재시작’ 버튼 클릭 시 처음부터 다시 시작

## 🛠 사용 기술
분야	사용 기술
엔진	Unity (버전 2022.3.4f1)
언어	C#
버전 관리	Git, GitHub
디자인	Aseprite (스프라이트 제작), Photoshop
기타 도구	Visual Studio, DoTween (UI 애니메이션), Audacity (사운드 편집)

Unity를 활용해 2.5D Top-Down 슈팅 게임을 제작

팀원들이 GitHub을 통해 협업 및 버전 관리를 수행

UI 및 애니메이션은 DoTween을 활용하여 부드럽고 직관적인 사용자 경험을 구현

사운드는 Audacity로 편집하고, SoundManager를 통해 효율적으로 제어
























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
│   └── EnemyUtil.cs  
  
