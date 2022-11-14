#pragma once

#include <iostream>
#include "rapidjson.h"
#include "document.h"

class MatchedInfo
{
	std::string m_ip;
	std::string m_port;
	std::string m_jsonString;
	MatchedInfo() = delete;
public:
	MatchedInfo(std::string ip, std::string port);
	std::string geteJsonString();
};

