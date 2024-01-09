﻿using QuazalServer.RDVServices.DDL.Models;
using QuazalServer.QNetZ;
using QuazalServer.QNetZ.Attributes;
using QuazalServer.QNetZ.Interfaces;

namespace QuazalServer.RDVServices.Services
{
    [RMCService(RMCProtocolId.MissionService)]
    public class MissionService : RMCServiceBase
    {
        [RMCMethod(1)]
        public RMCResult UKN1()
        {
            UNIMPLEMENTED();
            return Error(0);
        }

        [RMCMethod(3)]
        public RMCResult GetPersonaMissions()
        {
            return Result(new GetPersonaMissions());
        }

        [RMCMethod(4)]
        public RMCResult GetAllMissionTemplate()
        {
            return Result(new GetAllMissionTemplate());
        }

        [RMCMethod(7)]
        public RMCResult CancelMission()
        {
            return Result(new CancelMission());
        }
    }
}
