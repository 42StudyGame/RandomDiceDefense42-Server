#include "Redis.h"
#ifdef _MSC_VER
	#include <winsock2.h>
#endif

Redis::Redis(uint16_t isunix, std::string IP, uint16_t PORT, std::string ch)
	: m_isunix(isunix), m_IP(IP), m_PORT(PORT), m_ch(ch) {}

Redis::~Redis()
{
	redisFree(m_c);
}

bool Redis::RedisInit()
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
			std::cout << "Connection error: " << m_c->errstr << std::endl;
			redisFree(m_c);
		}
		else
		{
			std::cout << "Connection error: can't allocate redis context\n" << std::endl;
		}
		return false;
	}
	return true;
}

bool Redis::Publish(std::string input)
{
	redisReply* reply;

	std::string temp = "PUBLISH " + m_ch + " " + input;
	reply = (redisReply*)redisCommand(m_c, temp.c_str());
	if (reply == NULL)
	{
		return false;
	}
	freeReplyObject(reply);
	return true;
}
