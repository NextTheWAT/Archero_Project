# Archero\_Project

궁수의 전설 모티브 프로젝트

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
  
