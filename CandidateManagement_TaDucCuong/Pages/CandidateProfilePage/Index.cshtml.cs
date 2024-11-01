using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Candidate_BusinessObjects;
using Candidate_DAO;
using Candidate_Service;

namespace CandidateManagement_TaDucCuong.Pages.CandidateProfilePage
{
    public class IndexModel : PageModel
    {
        private readonly IHRCandidateService _hRCandidateService;

        public IndexModel(IHRCandidateService hRCandidateService)
        {
            _hRCandidateService = hRCandidateService;
        }

        public IList<CandidateProfile> CandidateProfile { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_hRCandidateService.GetCandidateProfiles() != null)
            {
                CandidateProfile= _hRCandidateService.GetCandidateProfiles();
            }
        }
    }
}
