using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Notebook
{
    public class NoteTitlesFragment : ListFragment
    {
        int selectedNoteId;
        bool showingTwoFragments;

        public NoteTitlesFragment()
        {
            // Being explicit about the requirement for a default constructor.
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            ListAdapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemActivated1, Notes.NoteTitles);

            if (savedInstanceState != null)
            {
                selectedNoteId = savedInstanceState.GetInt("current_note_id", 0);
            }

            var noteContainer = Activity.FindViewById(Resource.Id.note_container);
            showingTwoFragments = noteContainer != null &&
                                  noteContainer.Visibility == ViewStates.Visible;
            if (showingTwoFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                if (Notes.NoteContents.Count > 0 && Notes.NoteTitles.Count > 0)
                {
                    ShowNote(selectedNoteId);
                }
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_note_id", selectedNoteId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowNote(position);
        }

        void ShowNote(int noteId)
        {
            if (showingTwoFragments)
            {
                //ListView.SetItemChecked(selectedNoteId, true);

                var noteContentFragment = FragmentManager.FindFragmentById(Resource.Id.note_container) as NoteContentFragment;

                //Console.WriteLine(noteId);
                for(int i = 0; i < Notes.NoteContents.Count; i++)
                {
                    Console.WriteLine($"TEST {i}: {Notes.NoteContents[i]}");
                }

                if (noteContentFragment == null || noteContentFragment.NoteId != noteId)
                {
                    var container = Activity.FindViewById(Resource.Id.note_container);
                    var noteFrag = NoteContentFragment.NewInstance(selectedNoteId);

                    FragmentTransaction ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.note_container, noteFrag);
                    ft.Commit();
                }
            }
            else
            {
                var intent = new Intent(Activity, typeof(NoteContentActivity));
                intent.PutExtra("current_note_id", noteId);
                StartActivity(intent);
            }

        }
    }
}