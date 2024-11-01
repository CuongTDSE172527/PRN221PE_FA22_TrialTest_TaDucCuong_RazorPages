using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Candidate_BusinessObjects;

using Candidate_Service;

namespace CandidateManagement_TaDucCuong.Pages.CandidateProfilePage
{
    public class CreateModel : PageModel
    {
        private readonly IHRCandidateService profileservice;
        private readonly IJobpostService jobpost;

        public CreateModel(IHRCandidateService context, IJobpostService jobpostt)
        {
            profileservice = context;
            jobpost = jobpostt;
        }

        public IActionResult OnGet()
        {
        ViewData["PostingId"] = new SelectList(jobpost.GetJobPostings(), "PostingId", "JobPostingTitle");
            return Page();
        }

        [BindProperty]
        public CandidateProfile CandidateProfile { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || profileservice.GetCandidateProfiles() == null || CandidateProfile == null)
            {
                return Page();
            }

            profileservice.Create(CandidateProfile);
           

            return RedirectToPage("./Index");
        }
    }
}
