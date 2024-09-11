﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITemplate.Model.CustomException
{
	public class ForbiddenException : Exception
	{
		public ForbiddenException(string message = "İzinsiz işlem") : base(message)
		{

		}
	}
}
