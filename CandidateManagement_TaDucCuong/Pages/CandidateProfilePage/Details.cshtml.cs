﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly IHRCandidateService profileservice;

        public DetailsModel(IHRCandidateService context)
        {
            profileservice = context;
        }

      public CandidateProfile CandidateProfile { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || profileservice.GetCandidateProfiles() == null)
            {
                return NotFound();
            }

            var candidateprofile =  profileservice.GetJobCandidate(id);
            if (candidateprofile == null)
            {
                return NotFound();
            }
            else 
            {
                CandidateProfile = candidateprofile;
            }
            return Page();
        }
    }
}
