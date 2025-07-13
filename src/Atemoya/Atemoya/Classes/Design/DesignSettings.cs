using Atemoya.Classes.Entities;
using Atemoya.Classes.Helpers;
using System.Collections.ObjectModel;

namespace Atemoya.Classes.Design {

    public class DesignSettings {

        public DesignSettings() {

            Tags = new ObservableCollection<Gamertag> {
                new Gamertag(){
                    Name = "Unknown",
                    Email = "yYVG1D4hsZi6oKKnoeqfLau/ouUwXaTHY/hFsXXXFzMjz2amwcMOeXxwpv8pUrVWFSMvXTcXwYMPnNlJKExRnw==",
                    Password = "p5t3ca5ikX+lgkdke6VF4hw+e1yrw75sHTYKrP+qiLQ=",
                    Input = InputType.Controller,
                    Created = 0,
                    Updated = 0,
                    IsEditing = true,
                    IsNew = true
                }
            };

        }

        public ObservableCollection<Gamertag> Tags { get; set; }

    }

}