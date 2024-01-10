using Microsoft.AspNetCore.Mvc;
using Rsbc.Dmf.CaseManagement.Service;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using CaseDetail = Rsbc.Dmf.DriverPortal.ViewModels.CaseDetail;
using Pssg.DocumentStorageAdapter;
using AutoMapper;

namespace Rsbc.Dmf.DriverPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly CaseManager.CaseManagerClient _cmsAdapterClient;
        private readonly DocumentStorageAdapter.DocumentStorageAdapterClient _documentStorageAdapterClient;
        private readonly IMapper _mapper;
        private readonly ILogger<CasesController> _logger;

        public CasesController(
            IConfiguration configuration, 
            CaseManager.CaseManagerClient cmsAdapterClient,
            DocumentStorageAdapter.DocumentStorageAdapterClient documentStorageAdapterClient, 
            IMapper mapper, 
            ILoggerFactory loggerFactory
        ) {
            _configuration = configuration;
            _cmsAdapterClient = cmsAdapterClient;
            _documentStorageAdapterClient = documentStorageAdapterClient;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<CasesController>();
        }

        /// <summary>
        /// Get Case
        /// </summary>        
        [HttpGet("{caseId}")]
        [ProducesResponseType(typeof(CaseDetail), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ActionName("GetCase")]
        public ActionResult GetCase([Required] [FromRoute] string caseId)
        {
            var c = _cmsAdapterClient.GetCaseDetail(new CaseIdRequest { CaseId = caseId });

            if (c != null && c.ResultStatus == CaseManagement.Service.ResultStatus.Success)
            {
                var result = _mapper.Map<CaseDetail>(c.Item);
                return Json(result);
            }
            else
            {
                return StatusCode(500, c?.ErrorDetail ?? "GetCaseDetail failed.");
            }
        }

        /// <summary>
        /// Get closed documents for a given driver
        /// </summary>
        /// <param name="driverId">The driver id</param>
        /// <returns></returns>
        [HttpGet("{driverId}/Closed")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CaseDetail>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ActionName("GetClosedCases")]
        public ActionResult GetClosedCases([FromRoute] string driverId)
        {
            var caseStatusRequest = new CaseStatusRequest() { DriverId = driverId, Status = ActiveStatus.Closed };
            var reply = _cmsAdapterClient.GetCases(caseStatusRequest);
            if (reply.ResultStatus == CaseManagement.Service.ResultStatus.Success)
            {
                var result = new List<CaseDetail>();
                result = _mapper
                    .Map<IEnumerable<CaseDetail>>(reply.Items)
                    .ToList();

                return Json(result);
            }
            else
            {
                _logger.LogError($"{nameof(GetClosedCases)} failed for driverId: {driverId}", reply.ErrorDetail);
                return StatusCode(500, reply.ErrorDetail);
            }
        }

        /// <summary>
        /// Get Most Recent Case
        /// </summary>        
        [HttpGet("MostRecent")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ViewModels.CaseDetail), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ActionName("MostRecent")]
        public ActionResult GetMostRecentCase()
        {
            var result = new ViewModels.CaseDetail();

            result = new CaseDetail() {
                Title = "A1A1A1",
                CaseId = Guid.Empty.ToString(),
                DmerType = "Scheduled Age",
                CaseType = "Driver�s Medical Examination Report",
                LatestDecision = "Under review",
                OutstandingDocuments = 3
            };
            /*
            var c = _cmsAdapterClient.GetMostRecentCaseDetail(new DriverLicenseRequest { DriverLicenseNumber = driverLicenseNumber});
            if (c != null && c.ResultStatus == CaseManagement.Service.ResultStatus.Success)
            {
                string caseType = "Unsolicited";

                if (c.Item.CaseType == "DMER")
                {
                    caseType = "Solicited";
                }

                result.CaseId = c.Item.CaseId;
                result.Title = c.Item.Title;
                result.IdCode = c.Item.IdCode;
                result.OpenedDate = c.Item.OpenedDate.ToDateTimeOffset();
                result.CaseType = caseType;
                result.DmerType = c.Item.DmerType;
                result.Status = c.Item.Status;
                result.AssigneeTitle = c.Item.AssigneeTitle;
                result.LastActivityDate = c.Item.LastActivityDate.ToDateTimeOffset();
                result.LatestDecision = c.Item.LatestDecision;
                result.DecisionForClass = c.Item.DecisionForClass;
                result.DecisionDate = c.Item.DecisionDate.ToDateTimeOffset();
                result.OutstandingDocuments = (int)c.Item.OutstandingDocuments;
                if (c.Item.CaseSequence > -1)
                {
                    result.CaseSequence = (int)c.Item.CaseSequence;
                }

                result.DpsProcessingDate = c.Item.DpsProcessingDate.ToDateTimeOffset();

            }

            // set to null if no decision has been made.
            if (result.DecisionDate == DateTimeOffset.MinValue)
            {
                result.DecisionDate = null;
            }
            */
            return Json(result);
        }

        /// <summary>
        /// Returns the current user's cases
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(List<ViewModels.CaseDetail>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ActionName("GetCases")]
        public ActionResult GetCases()
        {
            var result = new List<ViewModels.CaseDetail>();
            result.Add(new CaseDetail() { Title = "Test-Title", CaseId = Guid.Empty.ToString(), DmerType = "DMER" });
            return Json(result);
        }

        /// <summary>
        /// Returns the current Closed Cases
        /// </summary>
        /// <returns></returns>
      /*  [HttpGet()]
        [ProducesResponseType(typeof(List<ViewModels.CaseDetail>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ActionName("GetClosedCases")]
        public ActionResult GetClosedCases()
        {
            var result = new List<ViewModels.CaseDetail>();
            result.Add(new CaseDetail() { 
                CaseId = Guid.Empty.ToString(),
                CaseType = "Driver�s Medical Examination Report",
                LatestDecision =    "Fit To Drive",
                EligibleLicenseClass = "5",
                OpenedDate = DateTime.Now,
                DecisionDate = DateTime.Now.AddDays(5),
            });
            return Json(result);
        }*/
    }

}