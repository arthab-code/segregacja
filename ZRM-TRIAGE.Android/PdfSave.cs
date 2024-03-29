﻿using Android;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Java.IO;
using PdfSharpCore.Pdf;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PdfSave))]

	public class PdfSave : IPdfSave
	{
		public void Save(string fileName, String contentType, MemoryStream stream)
		{
        string exception = string.Empty;
        string root = null;

        if (ContextCompat.CheckSelfPermission(Forms.Context, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
        {
            ActivityCompat.RequestPermissions((Android.App.Activity)Forms.Context, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
        }

        if (Android.OS.Environment.IsExternalStorageEmulated)
        {
            //root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            root = Android.App.Application.Context.GetExternalFilesDir(Environment.SystemDirectory).AbsolutePath;

        }
        else
            root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        Java.IO.File myDir = new Java.IO.File(root);
        myDir.Mkdir();

        Java.IO.File file = new Java.IO.File(myDir, fileName);

      //  if (file.Exists()) file.Delete();

        try
        {
            FileOutputStream outs = new FileOutputStream(file);
            outs.Write(stream.ToArray());
            outs.Flush();
            outs.Close();

            Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Plik pdf zapisano w:", file.ToString(), "OK");
           
            //OPEN PDF FILE FOR DEVICE VIEWER
            Xamarin.Essentials.Launcher.OpenAsync(new Xamarin.Essentials.OpenFileRequest() { File = new Xamarin.Essentials.ReadOnlyFile(file.AbsolutePath) }) ;
        }
        catch (Exception e)
        {
            exception = e.ToString();
        }

       /* if (file.Exists() && contentType != "application/html")
        {
            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
            string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            Android.Net.Uri path = FileProvider.GetUriForFile(Forms.Context, Android.App.Application.Context.PackageName + ".provider", file);
            intent.SetDataAndType(path, mimeType);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
        } */
    }
	}

