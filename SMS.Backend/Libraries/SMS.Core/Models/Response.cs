using System;
using System.Collections.Generic;

namespace SMS.Core.Models
{
	public class Response
	{
		public Response()
		{
			Errors = new List<string>();
			Succeded = true;
		}

		public bool Succeded { get; set; }

		public List<string> Errors { get; private set; }

		public void AddError(string error)
		{
			Errors.Add(error);
			Succeded = false;
		}

		public override string ToString()
		{
			string errors = string.Empty;

			if (Errors != null)
			{
				errors = string.Join(Environment.NewLine, Errors);
			}

			return errors;
		}
	}
}
