#include "RedisManager.h"
#include "MatchedInfo.h"
#ifdef _MSC_VER
	#include <winsock2.h>
#endif

RedisManager::RedisManager(uint16_t isunix, std::string IP, uint16_t PORT)
	: m_isunix(isunix), m_IP(IP), m_PORT(PORT)
{
}

RedisManager::~RedisManager()
{
	redisFree(m_c);
}

bool RedisManager::RedisInit()
{
	struct timeval timeout = { 1, 500000 }; // 1.5 seconds
	if (m_isunix)
	{
		m_c = redisConnectUnixWithTimeout(m_IP.c_str(), timeout);
	}
	else
	{
		m_c = redisConnectWithTimeout(m_IP.c_str(), m_PORT, timeout);
	}
	if (m_c == NULL || m_c->err)
	{
		if (m_c)
		{
			std::cerr << "\033[31mConnection error: " << m_c->errstr << "\033[0m" << std::endl;
			redisFree(m_c);
		}
		else
		{
			std::cerr << "\033[31mConnection error: can't allocate redis context\033[0m\n" << std::endl;
		}
		return false;
	}
	return true;
}

bool RedisManager::matching(std::string rank)
{
	int i = 0;
	redisReply* reply[4];
	std::string watch = "WATCH " + rank;
	std::string llen = "LLEN " + rank;
	std::string lrange = "LRANGE " + rank + " 0 1";
	std::string ltrim = "LTRIM " + rank + " 2 -1";

	while (1)
	{
		i = 0;
		// TODO: 요청이 계속 들어오거나 변경되면.... 매칭을 못시키게 되는거 아닌가?
		// Lock을 걸어버리게는 못하나
		redisCommand(m_c, watch.c_str());
		reply[i] = (redisReply*)redisCommand(m_c, lrange.c_str()); // i = 1
		if (reply[i]->elements < 2) // i = 0 
		{
			redisCommand(m_c, "UNWATCH");
			freeReplyObject(reply[i]);
			break;
		}
		MatchedInfo matched(std::string("127.0.0.1"), std::string("8181"));
		std::string json = matched.geteJsonString();
		std::string jsonCmd1 = "HSET matched " + std::string(reply[0]->element[0]->str) + " " + json;
		std::string jsonCmd2 = "HSET matched " + std::string(reply[0]->element[1]->str) + " " + json;
		redisCommand(m_c, "MULTI");
		reply[++i] = (redisReply*)redisCommand(m_c, jsonCmd1.c_str());
		reply[++i] = (redisReply*)redisCommand(m_c, jsonCmd2.c_str()); // i = 2
		reply[++i] = (redisReply*)redisCommand(m_c, ltrim.c_str()); // i = 3 이 상태로 빠져나옴
		redisCommand(m_c, "EXEC");
		redisCommand(m_c, "UNWATCH");
		for (int j = 0; j <= i; j++)
		{
			freeReplyObject(reply[j]);
		}
	}
	return true;
}

// 어떤 경우에 유저를 추가하게 될까 일단 스레드로 일정 시간 간격으로 확인하게는 게 좋을듯하다.
// radis확인하고 -> 각 랭크에 안맞게 집어넣는 작업 수행하는 . 스레드로 돌린다.
bool RedisManager::getUsersTask()
{
	if (matching("bronze") && matching("silver") && matching("gold"))
	{
		return true;
	}
	return false;
}
