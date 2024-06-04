﻿using pdipadapter.Infrastructure.HttpClients;
using MedicalPortal.API.Features.Endorsement.Model;
using MedicalPortal.API.Features.Endorsement.Services.Interfaces;
using Rsbc.Dmf.CaseManagement.Service;
using MapsterMapper;
using static MedicalPortal.API.Features.Endorsement.Model.EndorsementData.Model;
using Mapster;

namespace MedicalPortal.API.Features.Endorsement.Services;
public class Endorsement : BaseClient, IEndorsement
{
    private readonly UserManager.UserManagerClient userManager;
    private readonly IMapper mapper;
    public Endorsement(HttpClient client, ILogger<Endorsement> logger, UserManager.UserManagerClient userManager, IMapper mapper) : base(client, logger)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Model.Endorsement>> GetEndorsement(string hpDid)
    {
        hpDid = hpDid.Replace("@", "%40");
        var endorsementResult = await this.GetAsync<IEnumerable<EndorsementData.Model>>($"/api/v1/ext/parties/{hpDid}/endorsements").ConfigureAwait(false);

        if (!endorsementResult.IsSuccess || !endorsementResult.Value.Any())
        {
            if (!endorsementResult.IsSuccess)
            {
                // Log error or handle appropriately
                // TODO
            }
            else
            {
                this.Logger.LogNoEndorsementFound(hpDid);
            }
            return null;
        }

        var endorsementList = endorsementResult.Value;

        var practitionerRequests = endorsementList.Select(e => new PractitionerRequest { Hpdid = $"{e.Hpdid}" }).ToList();
        var contactTasks = practitionerRequests.Select(async endorsement =>
        {
            try
            {
                var contact = await userManager.GetPractitionerContactAsync(new PractitionerRequest { Hpdid = $"{endorsement.Hpdid}" }).ConfigureAwait(false);
                return contact;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return null;
            }
        }).ToList();

        var practitionerContacts = await Task.WhenAll(contactTasks).ConfigureAwait(false);

        var endorsements = endorsementList.Select((endorsement, index) => new Model.Endorsement
        {
            Hpdid = endorsement.Hpdid,
            Licences = endorsement.Licences.Select(licence => new LicenceInformation
            {
                IdentifierType = licence.IdentifierType,
                StatusCode = licence.StatusCode,
                StatusReasonCode = licence.StatusReasonCode
            }).ToList(),
            Contact = practitionerContacts[index]!.Adapt<Contact>() // Assuming you have a mapping method like "Adapt" here
        }).ToList();

        return endorsements;
    }

    public async Task<IEnumerable<Model.Endorsement>> GetEndorsements(string hpDid)
    {
        var endorsementList = await this.GetAsync<IEnumerable<EndorsementData.Model>>($"/api/v1/ext/parties/{hpDid}/endorsements");

        var practitionerRequests = endorsementList.Value.Select(e => new PractitionerRequest { Hpdid = $"{e.Hpdid}" }).ToList();
        var contactTasks = practitionerRequests.Select(async endorsement =>
        {
            var contact = await userManager.GetPractitionerContactAsync(new PractitionerRequest { Hpdid = $"{endorsement.Hpdid}" });
            return contact;
        }).ToList();

        var practitionerContacts = await Task.WhenAll(contactTasks);

        var endorsements = endorsementList.Value.Select((endorsement, index) => new Model.Endorsement
        {
            Hpdid = endorsement.Hpdid,
            Licences = endorsement.Licences.Select(licence => new LicenceInformation
            {
                IdentifierType = licence.IdentifierType,
                StatusCode = licence.StatusCode,
                StatusReasonCode = licence.StatusReasonCode
            }).ToList(),
            Contact = contactTasks[index].Result.Adapt<Contact>()
        }).ToList();

        // Return the mapped endorsements
        return endorsements;
    }

}

public static partial class JustinParticipantClientLoggingExtensions
{
    [LoggerMessage(1, LogLevel.Warning, "No Endorsement found in PiDP with Hpdid = {hpdid}.")]
    public static partial void LogNoEndorsementFound(this ILogger logger, string hpdid);

}