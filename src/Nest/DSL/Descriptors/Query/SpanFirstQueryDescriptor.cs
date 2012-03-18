﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class SpanFirstQueryDescriptor<T> : ISpanQuery where T : class
	{
		[JsonProperty(PropertyName = "match")]
		internal SpanQueryDescriptor<T> _SpanQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "end")]
		internal int? _End { get; set; }

		public SpanFirstQueryDescriptor<T> MatchTerm(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.MatchTerm(field, value, Boost: Boost);
			return this;
		}
		public SpanFirstQueryDescriptor<T> MatchTerm(string field, string value, double? Boost = null)
		{
			var span = new SpanQueryDescriptor<T>();
			span.SpanTerm(field, value, Boost);
			this._SpanQueryDescriptor = span;
			return this;
		}
		public SpanFirstQueryDescriptor<T> Match(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this._SpanQueryDescriptor = selector(new SpanQueryDescriptor<T>());
			return this;
		}
		public SpanFirstQueryDescriptor<T> End(int end)
		{
			this._End = end;
			return this;
		}

	}
}
