using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iycons_web2._0.Data; // Include your data context namespace
using Iycons_web2._0.Model;
using Iycons_web2._0.DTO;
using Microsoft.AspNetCore.Authorization;
using Iycons_web2._0.Service;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class FeedbackController : ControllerBase
{
    private readonly Context _context;

    public FeedbackController(Context context)
    {
        _context = context;
    }

    // GET: api/Feedback
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
    {
        return await _context.Feedbacks.ToListAsync();
    }

    // GET: api/Feedback/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Feedback>> GetFeedback(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);

        if (feedback == null)
        {
            return NotFound();
        }

        return feedback;
    }

    // POST: api/Feedback
    [HttpPost]
    public async Task<ActionResult<Feedback>> PostFeedback(FeedbackDto feedbackDto)
    {
        var feedback = new Feedback
        {
            Name = feedbackDto.Name,
            Message = feedbackDto.Message,
            Email = feedbackDto.Email,
            Subject = feedbackDto.Subject,
            PageId = feedbackDto.PageId,
            
            
        };

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        var emailService = new EmailService(); // You may need to inject this as a service.
        await emailService.SendFeedbackEmail(feedback);

        return Ok(feedback);
    }

    // PUT: api/Feedback/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFeedback(int id, Feedback feedbackDto)
    {
        if (id != feedbackDto.FeedbackId)
        {
            return BadRequest();
        }

        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null)
        {
            return NotFound();
        }

        // Update the feedback object with values from the DTO
        feedback.Name = feedbackDto.Name;
        feedback.Message = feedbackDto.Message;
        feedback.Email = feedbackDto.Email;
        feedback.Subject = feedbackDto.Subject;
        feedback.PageId = feedbackDto.PageId;

        _context.Entry(feedback).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FeedbackExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Feedback/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Feedback>> DeleteFeedback(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null)
        {
            return NotFound();
        }

        _context.Feedbacks.Remove(feedback);
        await _context.SaveChangesAsync();

        return feedback;
    }

    private bool FeedbackExists(int id)
    {
        return _context.Feedbacks.Any(e => e.FeedbackId == id);
    }
}
