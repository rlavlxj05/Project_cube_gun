# 유니티 제작한 모바일 3D 액션 게임입니다.

### 3D PC 액션게임입니다.

해당프로젝트 경우 포폴을 목적으로 유니티엔진에서 C#을 활용하여 UI 및 모든 시스템을 구축했습니다.

### 프로젝트 소개

플레이어가 스테이지를 올라가며 스테이지 수만큼 등장하는 적AI를 처치하고, 무기 및 아이템으로 생존하며 점수를 얻는 게임입니다.

영상 : https://youtu.be/WqDmZhVfKJM


### 코드구성

[몬스터]

https://github.com/rlavlxj05/Project_cube_gun/blob/master/Assets/2script/Enemy.cs

스위치문을 활용하여 각 몬스터마다 타입을 지정해 수정 및 관리하기 편하게 코드를 작성했습니다.

[보스]

https://github.com/rlavlxj05/Project_cube_gun/blob/master/Assets/2script/Boss.cs

스위치문을 활용하여 보스의 스킬들을 랜덤으로 실행되게 작성했습니다.

[게임매니저]

https://github.com/rlavlxj05/Project_cube_gun/blob/master/Assets/2script/Gamemanager.cs

전투시에 스위치문을 활용하여 몬스터가 랜덤하게 생성되도록 했으며 5스테이지마다 보스가 생성되도록 구현했습니다. 이 후 필드 위 몬스터가 없다면 상점 스테이지로 가도록 구현을 했습니다. 또한 UI 및 시간을 관리하도록 구성했습니다.


[무기]

https://github.com/rlavlxj05/Project_cube_gun/blob/master/Assets/2script/Weapon.cs

오브젝트를 활성화/비활성화로 교체할 수 있도록 했으며 활성화되있는 무기 태그에 따라 애니메이션이 다르게 작동되도록 했습니다.

[플레이어]

https://github.com/rlavlxj05/Project_cube_gun/blob/master/Assets/2script/player.cs

이동과 애니메이션이 잘 작동할 수 있도록 구현을 했습니다.

### 배운 점

1. 적AI, 충돌처리, 커멘드키셋팅 UI 등 다양한 시스템제작 코드의 활용도를 이해할 수 있었다.

2. 유니티 내 에니메이션을 활용 방법 및 연결 구조를 이해할 수 있었다.

### 앞으로 발전 가능성

현재 프로젝트에서 조이스틱과 버튼을 추가하여 모바일 버전으로 만들어 출시함과 출시 이후 부활 및 추가정수를 획득할 때 광고를 시청할 수 있도록 BM을 추가할 수 있을 것 같습니다.
