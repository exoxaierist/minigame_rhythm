# minigame_rhythm
파밍 2023-1 미니게임 1팀

현재 구현된 것들
EventObject -> 라운드 시작시, 게임이끝났을때 등 이벤트부르게끔 만들어둠
  ㄴGridObject -> 격자 딱 맞춰서 움직일수있게해줌
    ㄴControlledObject -> 키 입력에 대응하게함 (기본적인 이동도있음)
      ㄴUnitBase -> 유닛에대한 정보( 업그레이드, 종류, hp등 ) ( *미니게임 2팀을 위한 부분이라 지워야됨 )

Hp -> 오브젝트의 체력관리. UI표시도 함
HpUI -> Hp에서 자동적으로 만들어지고 ui부분만 관리함

Global -> 전역 변수, 레퍼런스 등 보관

InputHandler -> 어떤키눌렀을때 이벤트
AssetCollector -> 프리펩, 에셋 쉽게 레퍼런스할수있게 파일로 만든 오브젝트
AssetHolder -> 위의 콜렉터를 Global에서 레퍼런스할수있게 하기위함 
