using System;
using CarEnthusiast.Models;

namespace CarEnthusiast.Models
{
	public class GroupChatViewModel
	{
		public List<Group> UserGroups { get; set; }	

		public Group SelectedGroup { get; set; }

		public List<Message> Messages { get; set; }
	}
}
