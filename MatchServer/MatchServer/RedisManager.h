#pragma once

#include <typeinfo>
#include <iostream>
#include <deque>
#include "hiredis.h"

// redis에서 가지고 있는건 string형태. 이걸 c++로 가지고 나올때도 string형태이다.
//-> redis 매니저에 집어넣을때 : string형태.
// redis에서 가지고 나온 string를 클래스로 감싸서, 원소 및 수정할 수 있게 만들기
// redis반환값은 전용 free함수가 있다. 따라서.... Redis Mamager와, Radis로 받은 Json값에 대한 클래스가 따로 있어야 할 듯.
// RedisManager : 레디스 연결
// User : 레디스에서 꺼내고, 메모리로 가지고 있을 값들. UID, Rank(이후 추가로 연승 가중치 등등 추가)
// 꺼내서 어떻게 할건데? 일단 redis를 이용해서 같은 등급끼리 sorting을 시킬 수 있나? 일단 등급 같은거 2개 꺼낼 수 있다고 치자.
// 일단 내가 잘 정렬된거에서 뽑는다고 쳤을 때, 뽑고 그 두 유저를 redis에서 삭제하고, 매칭된 유저끼리 묶어서 DB에 넣기.
// 매칭서버는 매칭 대기 유저가 들어오면 DB에서 뽑아와서 vactor/dequeue형태로 가지고 있는다. - 동기화
// 매칭이 되면 대기 유저에서 유저가 있으면 삭제하고 매칭에 넣는다. 매칭 도중에 취소하면 대기에서 없애므로, 해당 유저를 다시 대기열에 넣는다
// 웹서버는 확인요청이 있을때 마다 매칭리스트에서 해당 유저가 있는지 확인한다.
// 따라서 일단 유저 정보형태 클래스를 따로 만들고 이걸 dequeue로 가지고 있는게 매번 비교할 때 편할듯.
class RedisManager
{
private:
	const uint16_t m_isunix = 0;
	const std::string m_IP;
	const uint16_t m_PORT;
	uint64_t m_keysCnt = 0;
	redisContext* m_c;
	RedisManager() = delete;
public:
	//Redis(uint16_t isunix, std::string IP, uint16_t PORT, std::string ch);
	RedisManager(uint16_t isunix, std::string IP, uint16_t PORT);
	~RedisManager();
	bool RedisInit();
	bool getUsersTask();
	bool matching(std::string rank);

};

