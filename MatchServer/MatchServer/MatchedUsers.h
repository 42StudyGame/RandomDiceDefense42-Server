#pragma once

#include <iostream>
#include "rapidjson.h"
#include "document.h"

class MatchedUsers
{
	std::string m_uid1;
	std::string m_uid2;
	std::string m_ip;
	std::string m_port;
	MatchedUsers() = delete;
public:
	MatchedUsers(std::string uid1, std::string uid2, std::string ip, std::string port);
	std::string makeJson();
};

