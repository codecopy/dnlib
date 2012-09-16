﻿using dot10.IO;

namespace dot10.DotNet.MD {
	/// <summary>
	/// Represents the #US stream
	/// </summary>
	public class USStream : DotNetStream {
		/// <inheritdoc/>
		public USStream(IImageStream imageStream, StreamHeader streamHeader)
			: base(imageStream, streamHeader) {
		}

		/// <summary>
		/// Reads a unicode string
		/// </summary>
		/// <param name="offset">Offset of unicode string</param>
		/// <returns>A string or <c>null</c> if <paramref name="offset"/> is invalid</returns>
		public string Read(uint offset) {
			if (offset == 0)
				return string.Empty;
			if (!IsValidOffset(offset))
				return null;
			imageStream.Position = offset;
			uint length;
			if (!imageStream.ReadCompressedUInt32(out length))
				return null;
			if (imageStream.Position + length < length || imageStream.Position + length > imageStream.Length)
				return null;
			return imageStream.ReadString((int)(length / 2));
		}

		/// <summary>
		/// Reads data just like <see cref="Read"/>, but returns an empty string if
		/// offset is invalid
		/// </summary>
		/// <param name="offset">Offset of unicode string</param>
		/// <returns>The string</returns>
		public string ReadNoNull(uint offset) {
			return Read(offset) ?? string.Empty;
		}
	}
}