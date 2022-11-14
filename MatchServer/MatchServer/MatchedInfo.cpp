
#include "MatchedInfo.h"

MatchedInfo::MatchedInfo(std::string ip, std::string port)
    : m_ip(ip), m_port(port)
{
    m_jsonString = "{\"ip\":\"" + m_ip + "\","
        + "\"port\":\"" + m_port + "\"}";
}

std::string MatchedInfo::geteJsonString()
{
    return m_jsonString;
}
