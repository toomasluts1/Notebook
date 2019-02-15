using Android.App;
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

            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.title_container, new NoteTitlesFragment());
            ft.Commit();

            var noteTitleText = FindViewById<EditText>(Resource.Id.noteTitleET);
            var addButton = FindViewById<Button>(Resource.Id.addNoteButton);

            addButton.Click += delegate
            {
                Notes.NoteTitles.Add(noteTitleText.Text);
                Notes.NoteContents.Add("");
                noteTitleText.Text = "";

                FragmentTransaction ft2 = this.FragmentManager.BeginTransaction();
                ft2.Replace(Resource.Id.title_container, new NoteTitlesFragment());
                ft2.Commit();
            };
        }
    }
}