using PdfSharpCore.Pdf;
using System;
using System.IO;

public interface IPdfSave
	{
		void Save(string fileName, String contentType, MemoryStream stream);
	}
