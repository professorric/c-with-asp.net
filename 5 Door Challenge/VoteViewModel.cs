using System.ComponentModel.DataAnnotations;

namespace DatarRadioCheck.Models
{

    public class VoteViewModel
    {
        [Required(ErrorMessage = "Please select an option.")]
        public string SelectedOption { get; set; }
    }

}
