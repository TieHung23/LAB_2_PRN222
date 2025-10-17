using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class FeedbackModel : PageModel
    {
        // Giả sử bạn sẽ tạo IFeedbackService
        // private readonly IFeedbackService _feedbackService;

        // public FeedbackModel(IFeedbackService feedbackService)
        // {
        //     _feedbackService = feedbackService;
        // }

        public IEnumerable<Feedback> Feedbacks { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Feedbacks = await _feedbackService.GetAllAsync();
            Feedbacks = new List<Feedback>();
        }
    }
}