﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OnlyJournal.Data.Habit
{
	public class HabitRecord
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Title { get; set; }
		public int SuccessCount { get; set; }
        public bool IsCompleted { get; set; }
		public string Description { get; set; }
	}
}
