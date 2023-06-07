using System;
using System.Collections.Generic;

namespace Web_Social_network_BE.Models;

public partial class Newfeed
{
	public long NewfeedId { get; set; }
	public string UserId { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public string Type { get; set; } = null!;

}
