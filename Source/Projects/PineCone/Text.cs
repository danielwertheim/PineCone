using System;

namespace PineCone
{
	[Serializable]
	public class Text : IEquatable<Text>
	{
		public string Value { get; private set; }

		public Text(string value)
		{
			Value = value;
		}

		public static implicit operator Text(string value)
		{
			return value == null
				? null
				: new Text(value);
		}

		public static implicit operator string(Text item)
		{
			return item == null
				? null
				: item.Value;
		}

		public override string ToString()
		{
			return Value;
		}

		public override bool Equals(object obj)
		{
			if (obj is string)
				return Equals(Value, obj as string);

			return Equals(obj as Text);
		}

		public bool Equals(Text other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Value, Value);
		}

		public override int GetHashCode()
		{
			return (Value != null ? Value.GetHashCode() : 0);
		}
	}
}