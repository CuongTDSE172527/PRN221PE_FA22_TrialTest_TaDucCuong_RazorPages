using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Candidate_BusinessObjects;
using Candidate_DAO;
using Candidate_Service;

namespace CandidateManagement_TaDucCuong.Pages.CandidateProfilePage
{
    public class EditModel : PageModel
    {
        private readonly IHRCandidateService profileservice;
        private readonly IJobpostService jobpost;

        public EditModel(IHRCandidateService context, IJobpostService jobpostt)
        {
            profileservice = context;
           jobpost = jobpostt;
        }

        [BindProperty]
        public CandidateProfile CandidateProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || profileservice.GetCandidateProfiles() == null)
            {
                return NotFound();
            }

            var candidateprofile = profileservice.GetJobCandidate(id);
            if (candidateprofile == null)
            {
                return NotFound();
            }
            CandidateProfile = candidateprofile;
           ViewData["PostingId"] = new SelectList(jobpost.GetJobPostings(), "PostingId", "JobPostingTitle");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            profileservice.UpdateCandidate(CandidateProfile);

            try
            {
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateProfileExists(CandidateProfile.CandidateId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CandidateProfileExists(string id)
        {
            var check = profileservice.GetJobCandidate(id);
            if(check == null)
            {
                return false;
            }
            return true;
        }
    }
}
