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
    public class NoteContentFragment : Fragment
    {
        public int NoteId => Arguments.GetInt("current_note_id", 0);

        public static NoteContentFragment NewInstance(int noteId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_note_id", noteId);
            return new NoteContentFragment { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            var editText = new EditText(Activity);
            var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
            editText.SetPadding(padding, padding, padding, padding);
            editText.TextSize = 24;

            if (Notes.NoteContents.Count > 0 && Notes.NoteTitles.Count > 0)
            {
                editText.Text = Notes.NoteContents[NoteId];
                editText.AfterTextChanged += delegate
                {
                    Notes.NoteContents[NoteId] = editText.Text;
                };
            }

            var scroller = new ScrollView(Activity);
            scroller.AddView(editText);

            return scroller;
        }
    }
}