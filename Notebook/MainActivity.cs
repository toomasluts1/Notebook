﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace Notebook
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var noteTitleText = FindViewById<EditText>(Resource.Id.noteTitleET);
            var addButton = FindViewById<Button>(Resource.Id.addNoteButton);

            addButton.Click += delegate
            {
                Notes.NoteTitles.Add(noteTitleText.Text);
                Notes.NoteContents.Add("");

                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.fragment3, new NoteTitlesFragment());
                ft.Commit();
            };
        }
    }
}