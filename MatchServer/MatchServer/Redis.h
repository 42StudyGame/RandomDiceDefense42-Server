#pragma once

#include <typeinfo>
#include <iostream>
#include "hiredis.h"

class Redis
{
private:
	const uint16_t m_isunix = 0;
	const std::string m_IP;
	const uint16_t m_PORT;
	const std::string m_ch;
	redisContext* m_c;
	Redis() = delete;
public:
	Redis(uint16_t isunix, std::string IP, uint16_t PORT, std::string ch);
	~Redis();
	bool RedisInit();
	bool Publish(std::string input);

};

