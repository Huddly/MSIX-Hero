﻿// MSIX Hero
// Copyright (C) 2022 Marcin Otorowski
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// Full notice:
// https://github.com/marcinotorowski/msix-hero/blob/develop/LICENSE.md

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Otor.MsixHero.Appx.Packaging.Manifest.Entities;

namespace Otor.MsixHero.App.Controls.PackageExpert.ViewModels.Items
{
    public class CapabilityViewModel
    {
        private static readonly Lazy<Geometry> DefaultIcon = new Lazy<System.Windows.Media.Geometry>(() => System.Windows.Media.Geometry.Parse("M 16 2.71875 L 12.5625 5.46875 L 8.9375 5.3125 L 8.125 5.25 L 7.9375 6.03125 L 7.03125 9.5 L 4.03125 11.46875 L 3.375 11.90625 L 3.65625 12.65625 L 4.9375 16 L 3.65625 19.34375 L 3.375 20.09375 L 4.03125 20.53125 L 7.03125 22.5 L 7.9375 25.96875 L 8.125 26.75 L 8.9375 26.6875 L 12.5625 26.53125 L 16 29.28125 L 19.4375 26.53125 L 23.0625 26.6875 L 23.875 26.75 L 24.0625 25.96875 L 24.96875 22.5 L 27.96875 20.53125 L 28.625 20.09375 L 28.34375 19.34375 L 27.0625 16 L 28.34375 12.65625 L 28.625 11.90625 L 27.96875 11.46875 L 24.96875 9.5 L 24.0625 6.03125 L 23.875 5.25 L 23.0625 5.3125 L 19.4375 5.46875 Z M 16 5.28125 L 18.46875 7.28125 L 18.78125 7.53125 L 19.15625 7.5 L 22.34375 7.34375 L 23.125 10.34375 L 23.21875 10.71875 L 23.53125 10.9375 L 26.1875 12.6875 L 25.0625 15.65625 L 24.9375 16 L 25.0625 16.34375 L 26.1875 19.3125 L 23.53125 21.0625 L 23.21875 21.28125 L 23.125 21.65625 L 22.34375 24.65625 L 19.15625 24.5 L 18.78125 24.46875 L 18.46875 24.71875 L 16 26.71875 L 13.53125 24.71875 L 13.21875 24.46875 L 12.84375 24.5 L 9.65625 24.65625 L 8.875 21.65625 L 8.78125 21.28125 L 8.46875 21.0625 L 5.8125 19.3125 L 6.9375 16.34375 L 7.0625 16 L 6.9375 15.65625 L 5.8125 12.6875 L 8.46875 10.9375 L 8.78125 10.71875 L 8.875 10.34375 L 9.65625 7.34375 L 12.84375 7.5 L 13.21875 7.53125 L 13.53125 7.28125 Z M 21.28125 12.28125 L 15 18.5625 L 11.71875 15.28125 L 10.28125 16.71875 L 14.28125 20.71875 L 15 21.40625 L 15.71875 20.71875 L 22.71875 13.71875 Z"));

        public CapabilityViewModel(AppxCapability capability)
        {
            this.Name = capability.Name;
            this.Type = capability.Type;
        }

        public string Name { get; }

        public string DisplayName
        {
            get => GetCapabilityDisplayNameFromEntity(this.Type, this.Name);
        }

        public Geometry VectorIcon
        {
            get => GetCapabilityVectorPathFromEntity(this.Type, this.Name);
        }

        public CapabilityType Type { get; }

        private static Geometry GetCapabilityVectorPathFromEntity(CapabilityType type, string name)
        {
            switch (name.ToLowerInvariant())
            {
                case "allowelevation":
                    // Windows 10 → Security → Shields → User Shield
                    return Geometry.Parse("M 16 4 C 13.75 4 12.234375 4.886719 10.875 5.625 C 9.515625 6.363281 8.28125 7 6 7 L 5 7 L 5 8 C 5 15.71875 7.609375 20.742188 10.25 23.78125 C 12.890625 26.820313 15.625 27.9375 15.625 27.9375 L 16 28.0625 L 16.375 27.9375 C 16.375 27.9375 19.109375 26.84375 21.75 23.8125 C 24.390625 20.78125 27 15.746094 27 8 L 27 7 L 26 7 C 23.730469 7 22.484375 6.363281 21.125 5.625 C 19.765625 4.886719 18.25 4 16 4 Z M 16 6 C 17.75 6 18.753906 6.613281 20.15625 7.375 C 21.339844 8.019531 22.910156 8.636719 24.9375 8.84375 C 24.746094 15.609375 22.507813 19.910156 20.25 22.5 C 18.203125 24.847656 16.484375 25.628906 16 25.84375 C 15.511719 25.625 13.796875 24.824219 11.75 22.46875 C 9.492188 19.871094 7.253906 15.578125 7.0625 8.84375 C 9.097656 8.636719 10.660156 8.019531 11.84375 7.375 C 13.246094 6.613281 14.25 6 16 6 Z M 16 10 C 14.355469 10 13 11.355469 13 13 C 13 13.617188 13.175781 14.207031 13.5 14.6875 C 12.015625 15.558594 11 17.164063 11 19 L 13 19 C 13 17.332031 14.332031 16 16 16 C 17.667969 16 19 17.332031 19 19 L 21 19 C 21 17.164063 19.984375 15.558594 18.5 14.6875 C 18.824219 14.207031 19 13.617188 19 13 C 19 11.355469 17.644531 10 16 10 Z M 16 12 C 16.5625 12 17 12.4375 17 13 C 17 13.5625 16.5625 14 16 14 C 15.4375 14 15 13.5625 15 13 C 15 12.4375 15.4375 12 16 12 Z");
                case "runfulltrust":
                    return Geometry.Parse("M 16 4 C 9.3844239 4 4 9.3844287 4 16 C 4 22.615571 9.3844239 28 16 28 C 22.615576 28 28 22.615571 28 16 C 28 9.3844287 22.615576 4 16 4 z M 16 6 C 21.534697 6 26 10.465307 26 16 C 26 21.534693 21.534697 26 16 26 C 10.465303 26 6 21.534693 6 16 C 6 10.465307 10.465303 6 16 6 z M 20.949219 12 L 14.699219 18.25 L 11.449219 15 L 10.050781 16.400391 L 14.699219 21.050781 L 22.349609 13.400391 L 20.949219 12 z");
                case "internetclientserver":
                case "internetclient":
                    // Windows 10 → User Interface → Open → Internet Connection
                    return Geometry.Parse("M 4 4 L 4 28 L 17.085938 28 C 18.251129 30.003907 20.255935 31.458128 22.617188 31.873047 L 22.667969 31.933594 L 22.75 31.892578 C 23.158294 31.95724 23.573998 32 24 32 C 26.857121 32 29.365635 30.484294 30.78125 28.220703 C 30.802215 28.189781 30.832301 28.157585 30.851562 28.126953 C 31.491593 27.110924 32.042732 25.758596 31.996094 24.074219 C 31.996325 24.049232 32 24.02504 32 24 C 32 21.050801 30.385813 18.473192 28 17.085938 L 28 4 L 4 4 z M 6 6 L 26 6 L 26 10 L 6 10 L 6 6 z M 7 7 L 7 9 L 9 9 L 9 7 L 7 7 z M 10 7 L 10 9 L 12 9 L 12 7 L 10 7 z M 13 7 L 13 9 L 25 9 L 25 7 L 13 7 z M 6 12 L 26 12 L 26 16.263672 C 25.359443 16.097511 24.691309 16 24 16 C 19.593562 16 16 19.593562 16 24 C 16 24.691309 16.097511 25.359443 16.263672 26 L 6 26 L 6 12 z M 25.228516 18.125 C 25.996064 18.28331 26.707552 18.588219 27.335938 19.007812 L 27.388672 19.582031 L 26.617188 19.253906 L 26.001953 19.785156 L 26.103516 21.255859 L 27.669922 20.775391 L 29.361328 21.308594 C 29.405175 21.396171 29.448655 21.484229 29.488281 21.574219 L 29.105469 22.271484 L 27.925781 21.560547 L 26.667969 21.738281 L 25.435547 22.650391 L 24.744141 24.78125 L 26.130859 25.921875 C 26.130859 25.921875 27.568531 25.669922 27.644531 25.669922 C 27.720531 25.669922 28.234375 26.962891 28.234375 26.962891 L 27.654297 28.757812 C 26.643262 29.533899 25.379608 30 24 30 C 23.49759 30 23.012586 29.931511 22.546875 29.816406 L 22.539062 29.804688 L 23.310547 26.964844 L 20.410156 24.808594 L 18.060547 24.808594 C 18.025088 24.543547 18 24.275104 18 24 C 18 23.485677 18.070951 22.989362 18.191406 22.513672 L 18.970703 21.896484 L 21.84375 20.552734 L 21.408203 18.675781 L 22.640625 18.396484 L 23.230469 19.208984 L 25.386719 18.804688 L 25.228516 18.125 z");
                case "microphone":
                case "backgroundmediareporting":
                    return Geometry.Parse("M 13 4 C 11.90625 4 11 4.90625 11 6 L 11 18 C 11 19.09375 11.90625 20 13 20 L 19 20 C 20.09375 20 21 19.09375 21 18 L 21 6 C 21 4.90625 20.09375 4 19 4 Z M 13 6 L 19 6 L 19 18 L 13 18 Z M 7 14 L 7 18 C 7 21.300781 9.699219 24 13 24 L 15 24 L 15 26 L 11 26 L 11 28 L 21 28 L 21 26 L 17 26 L 17 24 L 19 24 C 22.300781 24 25 21.300781 25 18 L 25 14 L 23 14 L 23 18 C 23 20.21875 21.21875 22 19 22 L 13 22 C 10.78125 22 9 20.21875 9 18 L 9 14 Z");
                case "webcam":
                    return Geometry.Parse("M 16 4 C 9.382813 4 4 9.382813 4 16 C 4 20.175781 6.132813 23.847656 9.375 26 L 9 26 L 9 28 L 23 28 L 23 26 L 22.625 26 C 25.867188 23.847656 28 20.175781 28 16 C 28 9.382813 22.617188 4 16 4 Z M 16 6 C 21.535156 6 26 10.464844 26 16 C 26 21.535156 21.535156 26 16 26 C 10.464844 26 6 21.535156 6 16 C 6 10.464844 10.464844 6 16 6 Z M 16 10 C 12.699219 10 10 12.699219 10 16 C 10 19.300781 12.699219 22 16 22 C 19.300781 22 22 19.300781 22 16 C 22 12.699219 19.300781 10 16 10 Z M 16 12 C 18.222656 12 20 13.777344 20 16 C 20 18.222656 18.222656 20 16 20 C 13.777344 20 12 18.222656 12 16 C 12 13.777344 13.777344 12 16 12 Z M 16 14 C 14.894531 14 14 14.894531 14 16 C 14 17.105469 14.894531 18 16 18 C 17.105469 18 18 17.105469 18 16 C 18 15.796875 17.960938 15.589844 17.90625 15.40625 C 17.746094 15.75 17.402344 16 17 16 C 16.449219 16 16 15.550781 16 15 C 16 14.597656 16.25 14.253906 16.59375 14.09375 C 16.410156 14.039063 16.203125 14 16 14 Z");
                case "packagequery":
                    // Windows 10 → Data → Database Operations → Database Import
                    return Geometry.Parse("M 6 4 L 6 28 L 16 28 L 16 26 L 8 26 L 8 21 L 16 21 L 16 19 L 8 19 L 8 13 L 19.171875 13 L 17.171875 11 L 8 11 L 8 6 L 22 6 L 22 4 L 6 4 z M 24 4 L 24 11 L 20 11 L 25 16 L 30 11 L 26 11 L 26 4 L 24 4 z M 21 17 L 21 18 L 18 18 L 18 32 L 32 32 L 32 18 L 29 18 L 29 17 L 27 17 L 27 18 L 23 18 L 23 17 L 21 17 z M 20 20 L 21 20 L 21 21 L 23 21 L 23 20 L 27 20 L 27 21 L 29 21 L 29 20 L 30 20 L 30 22 L 20 22 L 20 20 z M 20 24 L 30 24 L 30 30 L 20 30 L 20 24 z M 26 26 L 26 28 L 28 28 L 28 26 L 26 26 z");
                case "wificontrol":
                case "radios":
                    // Windows 10 → Network → Wireless Network → Wi-Fi
                    return Geometry.Parse("M 16 7 C 10.984375 7 6.457031 9.082031 3.1875 12.40625 L 4.59375 13.8125 C 7.5 10.851563 11.535156 9 16 9 C 20.464844 9 24.5 10.851563 27.40625 13.8125 L 28.8125 12.40625 C 25.542969 9.082031 21.015625 7 16 7 Z M 16 12 C 12.359375 12 9.082031 13.519531 6.71875 15.9375 L 8.125 17.34375 C 10.125 15.289063 12.914063 14 16 14 C 19.085938 14 21.875 15.289063 23.875 17.34375 L 25.28125 15.9375 C 22.917969 13.519531 19.640625 12 16 12 Z M 16 17 C 13.738281 17 11.707031 17.957031 10.25 19.46875 L 11.65625 20.875 C 12.75 19.726563 14.289063 19 16 19 C 17.710938 19 19.25 19.726563 20.34375 20.875 L 21.75 19.46875 C 20.296875 17.957031 18.261719 17 16 17 Z M 16 22 C 15.117188 22 14.332031 22.390625 13.78125 23 L 16 25.21875 L 18.21875 23 C 17.667969 22.390625 16.882813 22 16 22 Z");
                case "appcapturesettings":
                case "gamelist":
                case "gamebarservices":
                case "xboxaccessorymanagement":
                case "xboxliveauthenticationprovider":
                case "xboxgamespeechwindow":
                case "expandedresources":
                case "gamemonitor":
                    // Windows 10 → Gaming → Video Games → Game Controller
                    return Geometry.Parse("M 16 7 C 9.617188 7 4.03125 9.0625 4.03125 9.0625 L 3.4375 9.28125 L 3.375 9.875 L 2.03125 20.125 C 1.667969 22.960938 3.695313 25.605469 6.53125 25.96875 C 9.171875 26.308594 11.539063 24.527344 12.15625 22 L 19.84375 22 C 20.464844 24.527344 22.828125 26.308594 25.46875 25.96875 C 28.304688 25.605469 30.332031 22.960938 29.96875 20.125 L 28.625 9.875 L 28.5625 9.28125 L 27.96875 9.0625 C 27.96875 9.0625 22.382813 7 16 7 Z M 16 9 C 21.484375 9 26.007813 10.523438 26.75 10.78125 L 27.96875 20.40625 C 28.195313 22.167969 26.980469 23.742188 25.21875 23.96875 C 23.457031 24.195313 21.851563 22.980469 21.625 21.21875 L 21.59375 20.875 L 21.46875 20 L 10.53125 20 L 10.40625 20.875 L 10.375 21.21875 C 10.148438 22.980469 8.542969 24.195313 6.78125 23.96875 C 5.019531 23.742188 3.804688 22.167969 4.03125 20.40625 L 5.25 10.78125 C 5.992188 10.523438 10.515625 9 16 9 Z M 9 12 L 9 14 L 7 14 L 7 16 L 9 16 L 9 18 L 11 18 L 11 16 L 13 16 L 13 14 L 11 14 L 11 12 Z M 22 12 C 21.449219 12 21 12.449219 21 13 C 21 13.550781 21.449219 14 22 14 C 22.550781 14 23 13.550781 23 13 C 23 12.449219 22.550781 12 22 12 Z M 20 14 C 19.449219 14 19 14.449219 19 15 C 19 15.550781 19.449219 16 20 16 C 20.550781 16 21 15.550781 21 15 C 21 14.449219 20.550781 14 20 14 Z M 24 14 C 23.449219 14 23 14.449219 23 15 C 23 15.550781 23.449219 16 24 16 C 24.550781 16 25 15.550781 25 15 C 25 14.449219 24.550781 14 24 14 Z M 22 16 C 21.449219 16 21 16.449219 21 17 C 21 17.550781 21.449219 18 22 18 C 22.550781 18 23 17.550781 23 17 C 23 16.449219 22.550781 16 22 16 Z");
                case "phonecallhistorypublic":
                case "phonecallhistory":
                case "backgroundvoip":
                case "cellulardevicecontrol":
                case "cellulardeviceidentity":
                case "phonecall":
                case "phonecallsystem":
                case "phonelinetransportmanagement":
                case "oneprocessvoip":
                case "voipcall":
                case "hidtelephony":
                case "phonecallhistorysystem":
                case "dualsimtiles":
                    // Windows 10 → Mobile → Calls → Phone
                    return Geometry.Parse("M 8.65625 3 C 8.132813 3 7.617188 3.1875 7.1875 3.53125 L 7.125 3.5625 L 7.09375 3.59375 L 3.96875 6.8125 L 4 6.84375 C 3.035156 7.734375 2.738281 9.066406 3.15625 10.21875 C 3.160156 10.226563 3.152344 10.242188 3.15625 10.25 C 4.003906 12.675781 6.171875 17.359375 10.40625 21.59375 C 14.65625 25.84375 19.402344 27.925781 21.75 28.84375 L 21.78125 28.84375 C 22.996094 29.25 24.3125 28.960938 25.25 28.15625 L 28.40625 25 C 29.234375 24.171875 29.234375 22.734375 28.40625 21.90625 L 24.34375 17.84375 L 24.3125 17.78125 C 23.484375 16.953125 22.015625 16.953125 21.1875 17.78125 L 19.1875 19.78125 C 18.464844 19.433594 16.742188 18.542969 15.09375 16.96875 C 13.457031 15.40625 12.621094 13.609375 12.3125 12.90625 L 14.3125 10.90625 C 15.152344 10.066406 15.167969 8.667969 14.28125 7.84375 L 14.3125 7.8125 L 14.21875 7.71875 L 10.21875 3.59375 L 10.1875 3.5625 L 10.125 3.53125 C 9.695313 3.1875 9.179688 3 8.65625 3 Z M 8.65625 5 C 8.730469 5 8.804688 5.035156 8.875 5.09375 L 12.875 9.1875 L 12.96875 9.28125 C 12.960938 9.273438 13.027344 9.378906 12.90625 9.5 L 10.40625 12 L 9.9375 12.4375 L 10.15625 13.0625 C 10.15625 13.0625 11.304688 16.136719 13.71875 18.4375 L 13.9375 18.625 C 16.261719 20.746094 19 21.90625 19 21.90625 L 19.625 22.1875 L 22.59375 19.21875 C 22.765625 19.046875 22.734375 19.046875 22.90625 19.21875 L 27 23.3125 C 27.171875 23.484375 27.171875 23.421875 27 23.59375 L 23.9375 26.65625 C 23.476563 27.050781 22.988281 27.132813 22.40625 26.9375 C 20.140625 26.046875 15.738281 24.113281 11.8125 20.1875 C 7.855469 16.230469 5.789063 11.742188 5.03125 9.5625 C 4.878906 9.15625 4.988281 8.554688 5.34375 8.25 L 5.40625 8.1875 L 8.4375 5.09375 C 8.507813 5.035156 8.582031 5 8.65625 5 Z");
                case "bluetooth":
                    // Windows 10 → Network → Wireless Network → Bluetooth
                    return Geometry.Parse("M 14 3.59375 L 14 13.5625 L 9.71875 9.28125 L 8.28125 10.71875 L 13.5625 16 L 8.28125 21.28125 L 9.71875 22.71875 L 14 18.4375 L 14 28.40625 L 15.71875 26.71875 L 20.71875 21.71875 L 21.40625 21 L 20.71875 20.28125 L 16.4375 16 L 20.71875 11.71875 L 21.40625 11 L 20.71875 10.28125 L 15.71875 5.28125 Z M 16 8.4375 L 18.5625 11 L 16 13.5625 Z M 16 18.4375 L 18.5625 21 L 16 23.5625 Z");
                case "blockedchatmessages":
                case "chatsystem":
                case "smssend":
                case "cellularmessaging":
                    // Windows 10 → Mobile → Messages → SMS
                    return Geometry.Parse("M 4 4 L 4 20 L 8 20 L 8 24 L 17.648438 24 L 24 29.080078 L 24 24 L 28 24 L 28 8 L 24 8 L 24 4 L 4 4 z M 6 6 L 22 6 L 22 8 L 8 8 L 8 9 L 8 18 L 6 18 L 6 6 z M 10 10 L 26 10 L 26 22 L 22 22 L 22 24.917969 L 18.351562 22 L 10 22 L 10 10 z M 15 13 A 1 1 0 0 0 15 15 A 1 1 0 0 0 15 13 z M 21 13 A 1 1 0 0 0 21 15 A 1 1 0 0 0 21 13 z M 15.607422 17.205078 L 14.392578 18.794922 C 14.392578 18.794922 15.914756 20 18 20 C 20.085244 20 21.607422 18.794922 21.607422 18.794922 L 20.392578 17.205078 C 20.392578 17.205078 19.296756 18 18 18 C 16.703244 18 15.607422 17.205078 15.607422 17.205078 z");
                case "screenduplication":
                    // Windows 10 → Household → TV and Radio → Widescreen
                    return Geometry.Parse("M 2 7 L 2 23 L 30 23 L 30 7 Z M 4 9 L 28 9 L 28 21 L 4 21 Z M 10 24 L 10 26 L 22 26 L 22 24 Z");
                case "deviceportalprovider":
                case "appdiagnostics":
                case "deviceunlock":
                    // Windows 10 → User Interface → Basic Elements → System Diagnostic
                    return Geometry.Parse("M 5 5 L 5 6 L 5 27 L 27 27 L 27 5 L 5 5 z M 7 7 L 25 7 L 25 25 L 7 25 L 7 7 z M 10.5 9 A 1.5 1.5 0 0 0 9 10.542969 L 9 15.535156 L 12 17.535156 L 12 20 C 12 21.64497 13.35503 23 15 23 L 19 23 C 20.64497 23 22 21.64497 22 20 L 22 16.912109 A 1.5 1.5 0 0 0 21.5 14 A 1.5 1.5 0 0 0 20.001953 15.564453 L 20 15.564453 L 20 20 C 20 20.56503 19.56503 21 19 21 L 15 21 C 14.43497 21 14 20.56503 14 20 L 14 17.535156 L 17 15.535156 L 17 10.529297 L 16.998047 10.529297 A 1.5 1.5 0 0 0 15.5 9 A 1.5 1.5 0 0 0 15 11.914062 L 15 14.464844 L 13 15.798828 L 11 14.464844 L 11 11.912109 A 1.5 1.5 0 0 0 10.5 9 z");
                case "location":
                case "locationhistory":
                case "proximity":
                case "locationsystem":
                    // Windows 10 → Maps → Geotags → Marker
                    return Geometry.Parse("M 16 3 C 11.042969 3 7 7.042969 7 12 C 7 13.40625 7.570313 15.019531 8.34375 16.78125 C 9.117188 18.542969 10.113281 20.414063 11.125 22.15625 C 13.148438 25.644531 15.1875 28.5625 15.1875 28.5625 L 16 29.75 L 16.8125 28.5625 C 16.8125 28.5625 18.851563 25.644531 20.875 22.15625 C 21.886719 20.414063 22.882813 18.542969 23.65625 16.78125 C 24.429688 15.019531 25 13.40625 25 12 C 25 7.042969 20.957031 3 16 3 Z M 16 5 C 19.878906 5 23 8.121094 23 12 C 23 12.800781 22.570313 14.316406 21.84375 15.96875 C 21.117188 17.621094 20.113281 19.453125 19.125 21.15625 C 17.554688 23.867188 16.578125 25.300781 16 26.15625 C 15.421875 25.300781 14.445313 23.867188 12.875 21.15625 C 11.886719 19.453125 10.882813 17.621094 10.15625 15.96875 C 9.429688 14.316406 9 12.800781 9 12 C 9 8.121094 12.121094 5 16 5 Z M 16 10 C 14.894531 10 14 10.894531 14 12 C 14 13.105469 14.894531 14 16 14 C 17.105469 14 18 13.105469 18 12 C 18 10.894531 17.105469 10 16 10 Z");
                case "videoslibrary":
                    // Windows 10 → Photo and Video → Photos → Video Gallery
                    return Geometry.Parse("M 2 5 L 2 6 L 2 23 L 6 23 L 6 27 L 30 27 L 30 9 L 26 9 L 26 5 L 2 5 z M 4 7 L 24 7 L 24 21 L 4 21 L 4 7 z M 11 9.234375 L 11 11 L 11 18.765625 L 18.943359 14 L 11 9.234375 z M 26 11 L 28 11 L 28 25 L 8 25 L 8 23 L 26 23 L 26 11 z M 13 12.765625 L 15.056641 14 L 13 15.234375 L 13 12.765625 z");
                case "pictureslibrary":
                    // Windows 10 → Photo and Video → Photos → Photo Gallery
                    return Geometry.Parse("M 3 5 L 3 21 L 6.59375 21 L 6.5 22 L 7.5 22.09375 L 9.78125 22.34375 L 9.625 23.21875 L 9.4375 24.1875 L 10.40625 24.375 L 26.125 27.375 L 27.09375 27.5625 L 27.28125 26.59375 L 29.96875 12.78125 L 30.1875 11.8125 L 29.1875 11.625 L 25.8125 10.96875 L 26 9 L 26.09375 8.03125 L 25.125 7.90625 L 21 7.40625 L 21 5 Z M 5 7 L 19 7 L 19 19 L 5 19 Z M 12 9 C 10.355469 9 9 10.355469 9 12 C 9 12.671875 9.214844 13.308594 9.59375 13.8125 C 8.625 14.542969 8 15.691406 8 17 L 10 17 C 10 15.808594 10.808594 15 12 15 C 13.191406 15 14 15.808594 14 17 L 16 17 C 16 15.691406 15.375 14.542969 14.40625 13.8125 C 14.785156 13.308594 15 12.671875 15 12 C 15 10.355469 13.644531 9 12 9 Z M 21 9.40625 L 23.90625 9.78125 L 22.6875 21.6875 L 16.28125 21 L 21 21 Z M 12 11 C 12.5625 11 13 11.4375 13 12 C 13 12.5625 12.5625 13 12 13 C 11.4375 13 11 12.5625 11 12 C 11 11.4375 11.4375 11 12 11 Z M 25.59375 12.96875 L 27.84375 13.40625 L 25.5 25.21875 L 11.78125 22.59375 L 11.78125 22.53125 L 23.5 23.78125 L 24.5 23.90625 L 24.59375 22.90625 Z"); 
                case "objects3d":
                    // Windows 10 → Editing → 3D Objects → 3D Model
                    return Geometry.Parse("M 4 4 L 4 18.351562 L 8.5195312 24 L 24 24 L 24 23 L 24 8.5859375 L 19.414062 4 L 4 4 z M 7.4140625 6 L 18.585938 6 L 20.585938 8 L 9.4140625 8 L 7.4140625 6 z M 6 7.4140625 L 8 9.4140625 L 8 20.148438 L 6 17.648438 L 6 7.4140625 z M 10 10 L 22 10 L 22 22 L 10 22 L 10 10 z M 26.707031 13.292969 L 25.292969 14.707031 L 27.585938 17 L 25.292969 19.292969 L 26.707031 20.707031 L 30.414062 17 L 26.707031 13.292969 z M 13.707031 25.292969 L 12.292969 26.707031 L 16 30.414062 L 19.707031 26.707031 L 18.292969 25.292969 L 16 27.585938 L 13.707031 25.292969 z");
                case "useraccountinformation":
                case "documentslibrary":
                case "contacts":
                case "systemlevelcontactaccess":
                    // Windows 10 → Profile → Generic Users → User
                    return Geometry.Parse("M 16 5 C 12.144531 5 9 8.144531 9 12 C 9 14.410156 10.230469 16.550781 12.09375 17.8125 C 8.527344 19.34375 6 22.882813 6 27 L 8 27 C 8 24.109375 9.527344 21.59375 11.8125 20.1875 C 12.484375 21.835938 14.121094 23 16 23 C 17.878906 23 19.515625 21.835938 20.1875 20.1875 C 22.472656 21.59375 24 24.109375 24 27 L 26 27 C 26 22.882813 23.472656 19.34375 19.90625 17.8125 C 21.769531 16.550781 23 14.410156 23 12 C 23 8.144531 19.855469 5 16 5 Z M 16 7 C 18.773438 7 21 9.226563 21 12 C 21 14.773438 18.773438 17 16 17 C 13.226563 17 11 14.773438 11 12 C 11 9.226563 13.226563 7 16 7 Z M 16 19 C 16.820313 19 17.601563 19.117188 18.34375 19.34375 C 17.996094 20.308594 17.089844 21 16 21 C 14.910156 21 14.003906 20.308594 13.65625 19.34375 C 14.398438 19.117188 15.179688 19 16 19 Z");
                case "networkconnectionmanagerprovisioning":
                case "privatenetworkclientserver":
                case "networkdatausagemanagement":
                case "developmentmodenetwork":
                    // Windows 10 → Files → Operations → Network File System
                    return Geometry.Parse("M 9 3 L 9 21 L 10 21 L 23 21 L 23 7.0234375 L 18.941406 3 L 9 3 z M 11 5 L 18 5 L 18 8 L 21 8 L 21 19 L 11 19 L 11 5 z M 15 22 L 15 25.277344 C 14.699427 25.452081 14.452081 25.699427 14.277344 26 L 5 26 L 5 28 L 14.277344 28 C 14.623613 28.595634 15.261054 29 16 29 C 16.738946 29 17.376387 28.595634 17.722656 28 L 27 28 L 27 26 L 17.722656 26 C 17.547919 25.699427 17.300573 25.452081 17 25.277344 L 17 22 L 15 22 z");
                case "cortanapermissions":
                case "cortanaspeechaccessory":
                case "cortanasettings":
                    // Windows 10 → Logos → Microsoft → Cortana
                    return Geometry.Parse("M 16 3 A 1 1 0 0 0 15 4 A 1 1 0 0 0 16 5 A 1 1 0 0 0 17 4 A 1 1 0 0 0 16 3 z M 11.847656 3.7246094 A 1 1 0 0 0 11.554688 3.7851562 A 1 1 0 0 0 10.957031 5.0664062 A 1 1 0 0 0 12.238281 5.6640625 A 1 1 0 0 0 12.835938 4.3828125 A 1 1 0 0 0 11.847656 3.7246094 z M 20.082031 3.7246094 A 1 1 0 0 0 19.164062 4.3828125 A 1 1 0 0 0 19.761719 5.6640625 A 1 1 0 0 0 21.042969 5.0664062 A 1 1 0 0 0 20.445312 3.7851562 A 1 1 0 0 0 20.082031 3.7246094 z M 8.2890625 5.8066406 A 1 1 0 0 0 7.6445312 6.0410156 A 1 1 0 0 0 7.5214844 7.4492188 A 1 1 0 0 0 8.9296875 7.5722656 A 1 1 0 0 0 9.0527344 6.1640625 A 1 1 0 0 0 8.2890625 5.8066406 z M 23.740234 5.8085938 A 1 1 0 0 0 22.947266 6.1660156 A 1 1 0 0 0 23.070312 7.5742188 A 1 1 0 0 0 24.478516 7.4511719 A 1 1 0 0 0 24.355469 6.0429688 A 1 1 0 0 0 23.740234 5.8085938 z M 16 6 C 10.488997 6 6 10.488997 6 16 C 6 21.511003 10.488997 26 16 26 C 21.511003 26 26 21.511003 26 16 C 26 10.488997 21.511003 6 16 6 z M 16 8 C 20.430123 8 24 11.569877 24 16 C 24 20.430123 20.430123 24 16 24 C 11.569877 24 8 20.430123 8 16 C 8 11.569877 11.569877 8 16 8 z M 26.367188 9 A 1 1 0 0 0 25.890625 9.1328125 A 1 1 0 0 0 25.525391 10.5 A 1 1 0 0 0 26.890625 10.865234 A 1 1 0 0 0 27.257812 9.5 A 1 1 0 0 0 26.367188 9 z M 5.5605469 9.0019531 A 1 1 0 0 0 4.7421875 9.5 A 1 1 0 0 0 5.1074219 10.865234 A 1 1 0 0 0 6.4746094 10.5 A 1 1 0 0 0 6.1074219 9.1347656 A 1 1 0 0 0 5.5605469 9.0019531 z M 4.1855469 12.916016 A 1 1 0 0 0 3.1972656 13.742188 A 1 1 0 0 0 4.0078125 14.900391 A 1 1 0 0 0 5.1660156 14.089844 A 1 1 0 0 0 4.3554688 12.931641 A 1 1 0 0 0 4.1855469 12.916016 z M 27.841797 12.917969 A 1 1 0 0 0 27.642578 12.931641 A 1 1 0 0 0 26.832031 14.091797 A 1 1 0 0 0 27.990234 14.902344 A 1 1 0 0 0 28.802734 13.744141 A 1 1 0 0 0 27.841797 12.917969 z M 4.2070312 17.083984 A 1 1 0 0 0 4.0078125 17.099609 A 1 1 0 0 0 3.1953125 18.257812 A 1 1 0 0 0 4.3554688 19.068359 A 1 1 0 0 0 5.1660156 17.910156 A 1 1 0 0 0 4.2070312 17.083984 z M 27.820312 17.083984 A 1 1 0 0 0 26.832031 17.910156 A 1 1 0 0 0 27.644531 19.068359 A 1 1 0 0 0 28.802734 18.255859 A 1 1 0 0 0 27.990234 17.097656 A 1 1 0 0 0 27.820312 17.083984 z M 5.5839844 21 A 1 1 0 0 0 5.1074219 21.132812 A 1 1 0 0 0 4.7421875 22.5 A 1 1 0 0 0 6.1074219 22.865234 A 1 1 0 0 0 6.4746094 21.5 A 1 1 0 0 0 5.5839844 21 z M 26.345703 21.001953 A 1 1 0 0 0 25.527344 21.5 A 1 1 0 0 0 25.892578 22.867188 A 1 1 0 0 0 27.257812 22.5 A 1 1 0 0 0 26.892578 21.134766 A 1 1 0 0 0 26.345703 21.001953 z M 23.714844 24.191406 A 1 1 0 0 0 23.070312 24.425781 A 1 1 0 0 0 22.947266 25.833984 A 1 1 0 0 0 24.355469 25.958984 A 1 1 0 0 0 24.478516 24.548828 A 1 1 0 0 0 23.714844 24.191406 z M 8.3144531 24.195312 A 1 1 0 0 0 7.5214844 24.550781 A 1 1 0 0 0 7.6445312 25.960938 A 1 1 0 0 0 9.0527344 25.837891 A 1 1 0 0 0 8.9296875 24.427734 A 1 1 0 0 0 8.3144531 24.195312 z M 11.873047 26.275391 A 1 1 0 0 0 10.955078 26.933594 A 1 1 0 0 0 11.552734 28.214844 A 1 1 0 0 0 12.835938 27.617188 A 1 1 0 0 0 12.238281 26.335938 A 1 1 0 0 0 11.873047 26.275391 z M 20.054688 26.277344 A 1 1 0 0 0 19.761719 26.337891 A 1 1 0 0 0 19.164062 27.619141 A 1 1 0 0 0 20.445312 28.216797 A 1 1 0 0 0 21.042969 26.935547 A 1 1 0 0 0 20.054688 26.277344 z M 16 27 A 1 1 0 0 0 15 28 A 1 1 0 0 0 16 29 A 1 1 0 0 0 17 28 A 1 1 0 0 0 16 27 z");
                case "email":
                case "devicemanagementemailaccount":
                case "emailsystem":
                    // Windows 10 → Messaging → Envelopes → Group Message
                    return Geometry.Parse("M 2 5 L 2 23 L 6 23 L 6 27 L 30 27 L 30 9 L 26 9 L 26 5 Z M 4 7 L 24 7 L 24 9 L 6 9 L 6 21 L 4 21 Z M 10.09375 11 L 25.90625 11 L 18 16.75 Z M 8 11.96875 L 17.40625 18.8125 L 18 19.25 L 18.59375 18.8125 L 28 11.96875 L 28 25 L 8 25 Z");
                case "extendedexecutionbackgroundaudio":
                case "audiodeviceconfiguration":
                case "backgroundmediarecording":
                case "backgroundmediaplayback":
                    // Windows 10 → Media Controls → Sound → Audio
                    return Geometry.Parse("M 15 4.59375 L 13.28125 6.28125 L 8.5625 11 L 4 11 L 4 21 L 8.5625 21 L 13.28125 25.71875 L 15 27.40625 Z M 24.125 6.375 L 22.71875 7.78125 C 24.742188 9.929688 26 12.820313 26 16 C 26 19.179688 24.742188 22.070313 22.71875 24.21875 L 24.125 25.625 C 26.511719 23.113281 28 19.730469 28 16 C 28 12.269531 26.511719 8.886719 24.125 6.375 Z M 21.3125 9.1875 L 19.90625 10.625 C 21.207031 12.046875 22 13.925781 22 16 C 22 18.074219 21.207031 19.949219 19.90625 21.375 L 21.3125 22.8125 C 22.972656 21.027344 24 18.625 24 16 C 24 13.375 22.972656 10.972656 21.3125 9.1875 Z M 13 9.4375 L 13 22.5625 L 9.71875 19.28125 L 9.40625 19 L 6 19 L 6 13 L 9.40625 13 L 9.71875 12.71875 Z M 18.5 12.03125 L 17.0625 13.46875 C 17.640625 14.164063 18 15.027344 18 16 C 18 16.972656 17.640625 17.835938 17.0625 18.53125 L 18.5 19.96875 C 19.4375 18.910156 20 17.523438 20 16 C 20 14.476563 19.4375 13.089844 18.5 12.03125 Z");
                case "userdataaccountsprovider":
                case "appointments":
                case "appointmentssystem":
                    // Windows 10 → Time And Date → Basic Calendars → Calendar
                    return Geometry.Parse("M 9 4 L 9 5 L 5 5 L 5 27 L 27 27 L 27 5 L 23 5 L 23 4 L 21 4 L 21 5 L 11 5 L 11 4 Z M 7 7 L 9 7 L 9 8 L 11 8 L 11 7 L 21 7 L 21 8 L 23 8 L 23 7 L 25 7 L 25 9 L 7 9 Z M 7 11 L 25 11 L 25 25 L 7 25 Z M 13 13 L 13 15 L 15 15 L 15 13 Z M 17 13 L 17 15 L 19 15 L 19 13 Z M 21 13 L 21 15 L 23 15 L 23 13 Z M 9 17 L 9 19 L 11 19 L 11 17 Z M 13 17 L 13 19 L 15 19 L 15 17 Z M 17 17 L 17 19 L 19 19 L 19 17 Z M 21 17 L 21 19 L 23 19 L 23 17 Z M 9 21 L 9 23 L 11 23 L 11 21 Z M 13 21 L 13 23 L 15 23 L 15 21 Z M 17 21 L 17 23 L 19 23 L 19 21 Z");
                case "usb":
                case "removablestorage":
                    // Windows 10 → Computer Hardware → Data Storage → Usb Memory Stick
                    return Geometry.Parse("M 23.605469 2.4628906 L 19.5 6.5703125 L 17.515625 4.5859375 L 5.0507812 17.050781 C 2.3256076 19.775955 2.3256076 24.224045 5.0507812 26.949219 C 7.7759793 29.674275 12.224445 29.673993 14.949219 26.949219 L 27.414062 14.484375 L 25.429688 12.5 L 29.535156 8.3945312 L 23.605469 2.4628906 z M 23.605469 5.2929688 L 26.707031 8.3945312 L 24.015625 11.085938 L 20.914062 7.984375 L 23.605469 5.2929688 z M 17.515625 7.4140625 L 24.585938 14.484375 L 13.535156 25.535156 C 11.57433 27.495983 8.4257517 27.496901 6.4648438 25.535156 C 4.5040173 23.57433 4.5040174 20.42567 6.4648438 18.464844 L 17.515625 7.4140625 z M 10 19 C 9.0833335 19 8.268559 19.379756 7.7519531 19.960938 C 7.2353472 20.542119 7 21.277778 7 22 C 7 22.722222 7.2353472 23.457882 7.7519531 24.039062 C 8.268559 24.620244 9.0833335 25 10 25 C 10.916666 25 11.731441 24.620244 12.248047 24.039062 C 12.764653 23.457881 13 22.722222 13 22 C 13 21.277778 12.764653 20.542118 12.248047 19.960938 C 11.731441 19.379756 10.916666 19 10 19 z M 10 21 C 10.416666 21 10.601893 21.120244 10.751953 21.289062 C 10.902014 21.457881 11 21.722222 11 22 C 11 22.277778 10.90201 22.542118 10.751953 22.710938 C 10.601893 22.879756 10.416666 23 10 23 C 9.5833337 23 9.3981073 22.879756 9.2480469 22.710938 C 9.0979864 22.542119 9 22.277778 9 22 C 9 21.722222 9.0979864 21.457882 9.2480469 21.289062 C 9.3981073 21.120244 9.5833337 21 10 21 z");
                case "backgroundspatialperception":
                case "gazeinput":
                    // Windows 10 → Messaging → User Status → Eye
                    return Geometry.Parse("M 16 8 C 7.664063 8 1.25 15.34375 1.25 15.34375 L 0.65625 16 L 1.25 16.65625 C 1.25 16.65625 7.097656 23.324219 14.875 23.9375 C 15.246094 23.984375 15.617188 24 16 24 C 16.382813 24 16.753906 23.984375 17.125 23.9375 C 24.902344 23.324219 30.75 16.65625 30.75 16.65625 L 31.34375 16 L 30.75 15.34375 C 30.75 15.34375 24.335938 8 16 8 Z M 16 10 C 18.203125 10 20.234375 10.601563 22 11.40625 C 22.636719 12.460938 23 13.675781 23 15 C 23 18.613281 20.289063 21.582031 16.78125 21.96875 C 16.761719 21.972656 16.738281 21.964844 16.71875 21.96875 C 16.480469 21.980469 16.242188 22 16 22 C 15.734375 22 15.476563 21.984375 15.21875 21.96875 C 11.710938 21.582031 9 18.613281 9 15 C 9 13.695313 9.351563 12.480469 9.96875 11.4375 L 9.9375 11.4375 C 11.71875 10.617188 13.773438 10 16 10 Z M 16 12 C 14.34375 12 13 13.34375 13 15 C 13 16.65625 14.34375 18 16 18 C 17.65625 18 19 16.65625 19 15 C 19 13.34375 17.65625 12 16 12 Z M 7.25 12.9375 C 7.09375 13.609375 7 14.285156 7 15 C 7 16.753906 7.5 18.394531 8.375 19.78125 C 5.855469 18.324219 4.105469 16.585938 3.53125 16 C 4.011719 15.507813 5.351563 14.203125 7.25 12.9375 Z M 24.75 12.9375 C 26.648438 14.203125 27.988281 15.507813 28.46875 16 C 27.894531 16.585938 26.144531 18.324219 23.625 19.78125 C 24.5 18.394531 25 16.753906 25 15 C 25 14.285156 24.90625 13.601563 24.75 12.9375 Z");
            }
            
            return DefaultIcon.Value;
        }

        private static string GetCapabilityDisplayNameFromEntity(CapabilityType type, string name)
        {
            switch (name)
            {
                case "usb":
                    return Resources.Localization.Capability_USB;
                case "humaninterfacedevice":
                    return Resources.Localization.Capability_HID;
                case "pointOfService":
                    return Resources.Localization.Capability_POS;
                case "wiFiControl":
                    return Resources.Localization.Capability_WiFi;
                case "radios":
                    return Resources.Localization.Capability_RadioState;
                case "optical":
                    return Resources.Localization.Capability_OpticalDisc;
                case "activity":
                    return Resources.Localization.Capability_MotionActivity;
                case "serialcommunication":
                    return Resources.Localization.Capability_SerialCommunication;
                case "gazeInput":
                    return Resources.Localization.Capability_Gaze;
                case "lowLevel":
                    return Resources.Localization.Capability_LowLevel;
                case "documentsLibrary":
                    return Resources.Localization.Capability_Documents;
                case "appCaptureSettings":
                    return Resources.Localization.Capability_GameDvr;
                case "cellularDeviceControl":
                    return Resources.Localization.Capability_Cellular;
                case "cellularDeviceIdentity":
                    return Resources.Localization.Capability_CellularIdentity;
                case "cellularMessaging":
                    return Resources.Localization.Capability_CellularMsg;
                case "dualSimTiles":
                    return Resources.Localization.Capability_DualSim;
                case "enterpriseDeviceLockdown":
                    return Resources.Localization.Capability_EnterpriseSharedStorage;
                case "inputInjectionBrokered":
                    return Resources.Localization.Capability_SysInputInjection;
                case "inputObservation":
                    return Resources.Localization.Capability_ObserveInput;
                case "inputSuppression":
                    return Resources.Localization.Capability_SuppressInput;
                case "networkingVpnProvider":
                    return Resources.Localization.Capability_VPN;
                case "packageManagement":
                    return Resources.Localization.Capability_ManageOtherApps;
                case "packageQuery":
                    return Resources.Localization.Capability_GatherInfoOtherApps;
                case "screenDuplication":
                    return Resources.Localization.Capability_ScreenProjection;
                case "location":
                    return Resources.Localization.Capability_Location;
                case "webcam":
                    return Resources.Localization.Capability_Webcam;
                case "microphone":
                    return Resources.Localization.Capability_Microphone;
                case "userPrincipalName":
                    return Resources.Localization.Capability_UserPrincipalName;
                case "walletSystem":
                    return Resources.Localization.Capability_Wallet;
                case "locationHistory":
                    return Resources.Localization.Capability_LocationHistory;
                case "confirmAppClose":
                    return Resources.Localization.Capability_AppCloseConfirmation;
                case "phoneCallHistory":
                    return Resources.Localization.Capability_CallHistory;
                case "appointmentsSystem":
                    return Resources.Localization.Capability_SystemAppointment;
                case "chatSystem":
                    return Resources.Localization.Capability_SystemChat;
                case "contactsSystem":
                    return Resources.Localization.Capability_SystemContacts;
                case "email":
                    return Resources.Localization.Capability_Email;
                case "emailSystem":
                    return Resources.Localization.Capability_SystemEmail;
                case "phoneCallHistorySystem":
                    return Resources.Localization.Capability_SystemCallHistory;
                case "smsSend":
                    return Resources.Localization.Capability_Sms;
                case "userDataSystem":
                    return Resources.Localization.Capability_SystemAllUsers;
                case "previewStore":
                    return Resources.Localization.Capability_StorePreview;
                case "firstSignInSettings":
                    return Resources.Localization.Capability_FirstTimeSignIn;
                case "teamEditionExperience":
                    return Resources.Localization.Capability_WindowsTeamExperience;
                case "remotePassportAuthentication":
                    return Resources.Localization.Capability_RemoteUnlock;
                case "runFullTrust":
                    return Resources.Localization.Capability_FullTrust;
                case "allowElevation":
                    return Resources.Localization.Capability_Elevation;
                case "teamEditionDeviceCredential":
                    return Resources.Localization.Capability_WindowsTeamDeviceCreds;
                case "teamEditionView":
                    return Resources.Localization.Capability_WindowsTeamAppView;
                case "cameraProcessingExtension":
                    return Resources.Localization.Capability_CameraProcExt;
                case "musicLibrary":
                    return Resources.Localization.Capability_Music;
                case "picturesLibrary":
                    return Resources.Localization.Capability_Pictures;
                case "videosLibrary":
                    return Resources.Localization.Capability_Videos;
                case "removableStorage":
                    return Resources.Localization.Capability_RemovableStorage;
                case "internetClient":
                    return Resources.Localization.Capability_InternetClient;
                case "internetClientServer":
                    return Resources.Localization.Capability_InternetClientServer;
                case "privateNetworkClientServer":
                    return Resources.Localization.Capability_HomeNetwork;
                case "phoneCall":
                    return Resources.Localization.Capability_PhoneCalls;
                case "phoneCallHistoryPublic":
                    return Resources.Localization.Capability_PhoneCallHistory;
                case "voipCall":
                    return Resources.Localization.Capability_VoIP;
                case "objects3D":
                    return Resources.Localization.Capability_3d;
                case "blockedChatMessages":
                    return Resources.Localization.Capability_ReadBlockedMessages;
                case "lowLevelDevices":
                    return Resources.Localization.Capability_CustomDevices;
                case "systemManagement":
                    return Resources.Localization.Capability_IoT;
                case "backgroundMediaPlayback":
                    return Resources.Localization.Capability_BackgroundMedia;
                case "phoneLineTransportManagement":
                    return Resources.Localization.Capability_ManagePhoneLine;
                case "networkDataUsageManagement":
                    return Resources.Localization.Capability_DataUsageMgmt;
                case "smbios":
                    return Resources.Localization.Capability_Bios;
                case "broadFileSystemAccess":
                    return Resources.Localization.Capability_BroadFs;
                case "developmentModeNetwork":
                    return Resources.Localization.Capability_DevelopmentModeNetwork;
                case "oneProcessVoIP":
                    return Resources.Localization.Capability_ReserveVoIP;
                case "backgroundVoIP":
                    return Resources.Localization.Capability_AutoVoIP;
                case "enterpriseCloudSSO":
                    return Resources.Localization.Capability_EnterpriseSSO;
                case "uiAutomation":
                    return Resources.Localization.Capability_UIAutomation;
                case "userSystemId":
                    return Resources.Localization.Capability_UserSystemID;
                case "previewPenWorkspace":
                    return Resources.Localization.Capability_PenWorkspace;
                case "appLicensing":
                    return Resources.Localization.Capability_AppLicensing;
                case "oemDeployment":
                    return Resources.Localization.Capability_OemDeploy;
                case "userSigninSupport":
                    return Resources.Localization.Capability_UserSignIn;
                case "inputForegroundObservation":
                    return Resources.Localization.Capability_ForegroundObservation;
                case "interopServices":
                    return Resources.Localization.Capability_DriverAccess;
                case "accessoryManager":
                    return Resources.Localization.Capability_AccessoryManagement;
                case "cortanaSpeechAccessory":
                    return Resources.Localization.Capability_SpeechRecognition;
                case "xboxAccessoryManagement":
                    return Resources.Localization.Capability_XboxAccessory;
                case "gameList":
                    return Resources.Localization.Capability_GamesList;
                case "packagePolicySystem":
                    return Resources.Localization.Capability_PackagePolicyControl;
                case "deviceManagementDmAccount":
                    return Resources.Localization.Capability_ProvisionConfigureOmaDm;
                case "deviceManagementFoundation":
                    return Resources.Localization.Capability_MdmCspInf;
                case "deviceManagementWapSecurityPolicies":
                    return Resources.Localization.Capability_ConfigureWAP;
                case "deviceManagementEmailAccount":
                    return Resources.Localization.Capability_ManageEmailAccount;
                case "extendedBackgroundTaskTime":
                    return Resources.Localization.Capability_ExtendedBgTaskTime;
                case "extendedExecutionBackgroundAudio":
                    return Resources.Localization.Capability_ExtendedExecBgAudio;
                case "slapiQueryLicenseValue":
                    return Resources.Localization.Capability_SoftwareLicensing;
                case "inProcessMediaExtension":
                    return Resources.Localization.Capability_InProcMediaExt;
                case "hidTelephony":
                    return Resources.Localization.Capability_HidTelephony;
                default:

                    var regex = Regex.Matches(name, "[A-Z](?![A-Z])");
                    var sb = new StringBuilder();

                    var previous = name.Length;
                    for (var i = regex.Count - 1; i >= 0; i--)
                    {
                        sb.Insert(0, name.Substring(regex[i].Index, previous - regex[i].Index));
                        previous = regex[i].Index;

                        if (previous > 0)
                        {
                            sb.Insert(0, " ");
                        }
                    }

                    if (previous > 0)
                    {
                        sb.Insert(0, name.Substring(0, previous));
                    }

                    var trimmed = sb.ToString().Trim();
                    if (char.IsUpper(trimmed[0]))
                    {
                        return trimmed;
                    }

                    return trimmed.Substring(0, 1).ToUpperInvariant() + trimmed.Substring(1);
            }
        }
    }
}
