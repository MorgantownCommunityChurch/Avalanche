﻿// <copyright>
// Copyright Southeast Christian Church
//
// Licensed under the  Southeast Christian Church License (the "License");
// you may not use this file except in compliance with the License.
// A copy of the License shoud be included with this file.
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;
using System.Text;
using Avalanche.CustomControls;
using Avalanche.Interfaces;
using Xamarin.Forms;

namespace Avalanche.Blocks
{
    public class VideoPlayerBlock : Avalanche.CustomControls.VideoPlayer, IRenderable, IHasMedia
    {
        public Dictionary<string, string> Attributes { get; set; }
        public VideoPlayerBlock()
        {
        }

        public event EventHandler<bool> FullScreenChanged;

        private bool allocated = false;
        protected override void OnSizeAllocated( double width, double height )
        {
            base.OnSizeAllocated( width, height );
            if ( !allocated )
            {
                allocated = true;
                HeightRequest = Math.Min( width * this.AspectRatio, App.Current.MainPage.Height );
            }
        }

        public View Render()
        {
            this.Prepared += VideoPlayerBlock_Prepared;
            this.FullScreenStatusChanged += VideoPlayerBlock_FullScreenStatusChanged;
            return this;
        }

        private void VideoPlayerBlock_FullScreenStatusChanged( object sender, bool e )
        {
            allocated = false;
            FullScreenChanged?.Invoke( this, e );
        }

        private void VideoPlayerBlock_Prepared( object sender, EventArgs e )
        {
            if ( AutoPlay )
            {
                this.Play();
            }
        }

        public void BackButtonPressed()
        {
            if ( IsFullScreen )
            {
                ExitFullScreen();
            }
        }

        public void PageDisappeared()
        {
            this.Stop();
        }
    }
}
