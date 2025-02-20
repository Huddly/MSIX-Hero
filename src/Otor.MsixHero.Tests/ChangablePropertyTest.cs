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
using System.Linq;
using NUnit.Framework;
using Otor.MsixHero.App.Mvvm.Changeable;

namespace Otor.MsixHero.Tests
{
    [TestFixture()]
    public class ChangablePropertyTest
    {
        [Test]
        public void ValidateObservableCollectionBool()
        {
            var col1 = new[] { true, true, true };
            var changeableCol = new ChangeableCollection<bool>(col1);

            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol.Add(true);
            Assert.IsTrue(changeableCol.IsTouched);
            Assert.IsTrue(changeableCol.IsDirty);

            changeableCol.RemoveAt(3);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(2);
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Add(true);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol[0] = false;
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol[0] = true;
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(0);
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Reset(ValueResetType.Soft);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(0);

            changeableCol.Reset(ValueResetType.Hard);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol.Add(true);
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Commit();
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);
        }

        [Test]
        public void ValidateObservableCollectionChangeables()
        {
            var col1 = new[] { "string1", "string2", "string3" };
            var changeableCol = new ChangeableCollection<ChangeableProperty<string>>(col1.Select(c => new ChangeableProperty<string>(c)));

            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol.Add(new ChangeableProperty<string>("string4"));
            Assert.IsTrue(changeableCol.IsTouched);
            Assert.IsTrue(changeableCol.IsDirty);

            changeableCol.RemoveAt(3);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(2);
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Add(new ChangeableProperty<string>("string3"));
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Reset(ValueResetType.Hard);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol[0].CurrentValue = "string1a";
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Reset(ValueResetType.Hard);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol[0].CurrentValue = "string1";
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol[0].CurrentValue = "string1a";
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol[0].CurrentValue = "string1";
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol[0].Reset(ValueResetType.Hard);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched, "Resetting a single dirty item should set the dirty flag to true, but touched should remain.");
            
            changeableCol.Reset();
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol[0].CurrentValue = "aaa";
            changeableCol[1].CurrentValue = "bbb";
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);
            changeableCol[0].Reset();
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);
            changeableCol[1].Reset();
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(0);
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Reset(ValueResetType.Soft);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(0);

            changeableCol.Reset(ValueResetType.Hard);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol.Add(new ChangeableProperty<string>("string4"));
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Commit();
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);
        }

        [Test]
        public void ValidateObservableCollectionString()
        {
            var col1 = new[] {"string1", "string2", "string3"};
            var changeableCol = new ChangeableCollection<string>(col1);

            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol.Add("string4");
            Assert.IsTrue(changeableCol.IsTouched);
            Assert.IsTrue(changeableCol.IsDirty);

            changeableCol.Remove("string4");
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);
            
            changeableCol.Remove("string3");
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Add("string3");
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol[0] = "string1a";
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol[0] = "string1";
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(0);
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Reset(ValueResetType.Soft);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.RemoveAt(0);

            changeableCol.Reset(ValueResetType.Hard);
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);

            changeableCol.Add("string4");
            Assert.IsTrue(changeableCol.IsDirty);
            Assert.IsTrue(changeableCol.IsTouched);

            changeableCol.Commit();
            Assert.IsFalse(changeableCol.IsDirty);
            Assert.IsFalse(changeableCol.IsTouched);
        }

        [Test]
        public void ValidatorTest()
        {
            Func<int, string> validator = i => i % 2 != 0 ? "The number must be even" : null;

            var validatedProperty = new ValidatedChangeableProperty<int>("abc", 0, validator);
            Assert.IsTrue(validatedProperty.IsValidated);
            Assert.IsTrue(validatedProperty.IsValid);
            Assert.IsNull(validatedProperty.ValidationMessage);

            validatedProperty.CurrentValue = 1;
            Assert.IsTrue(validatedProperty.IsValidated);
            Assert.IsFalse(validatedProperty.IsValid);
            Assert.AreEqual("abc: The number must be even", validatedProperty.ValidationMessage);

            validatedProperty.IsValidated = false;
            Assert.IsFalse(validatedProperty.IsValidated);
            Assert.IsTrue(validatedProperty.IsValid);
            Assert.IsNull(validatedProperty.ValidationMessage);
        }

        [Test]
        public void ChangeableContainerTest()
        {
            var prop1 = new ChangeableProperty<string>("initial");
            var prop2 = new ChangeableProperty<string>("initial");

            var container = new ChangeableContainer(prop1, prop2);
            Assert.IsFalse(prop1.IsTouched);
            Assert.IsFalse(prop2.IsTouched);
            Assert.IsFalse(container.IsTouched);
            Assert.IsFalse(prop1.IsDirty);
            Assert.IsFalse(prop2.IsDirty);
            Assert.IsFalse(container.IsDirty);

            prop1.CurrentValue = "other";
            Assert.IsTrue(prop1.IsTouched);
            Assert.IsFalse(prop2.IsTouched);
            Assert.IsTrue(container.IsTouched);
            Assert.IsTrue(prop1.IsDirty);
            Assert.IsFalse(prop2.IsDirty);
            Assert.IsTrue(container.IsDirty);

            prop1.CurrentValue = "initial";
            Assert.IsTrue(prop1.IsTouched);
            Assert.IsFalse(prop2.IsTouched);
            Assert.IsTrue(container.IsTouched);
            Assert.IsFalse(prop1.IsDirty);
            Assert.IsFalse(prop2.IsDirty);
            Assert.IsFalse(container.IsDirty);

            prop1.Reset(ValueResetType.Hard);
            Assert.IsFalse(prop1.IsTouched);
            Assert.IsFalse(prop2.IsTouched);
            Assert.IsFalse(container.IsTouched);
            Assert.IsFalse(prop1.IsDirty);
            Assert.IsFalse(prop2.IsDirty);
            Assert.IsFalse(container.IsDirty);

            prop2.CurrentValue = "second";
            Assert.IsFalse(prop1.IsTouched);
            Assert.IsTrue(prop2.IsTouched);
            Assert.IsTrue(container.IsTouched);
            Assert.IsFalse(prop1.IsDirty);
            Assert.IsTrue(prop2.IsDirty);
            Assert.IsTrue(container.IsDirty);

            container.Reset(ValueResetType.Hard);
            Assert.IsFalse(prop1.IsTouched);
            Assert.IsFalse(prop2.IsTouched);
            Assert.IsFalse(container.IsTouched);
            Assert.IsFalse(prop1.IsDirty);
            Assert.IsFalse(prop2.IsDirty);
            Assert.IsFalse(container.IsDirty);

            prop2.CurrentValue = "second";
            container.Commit();
            Assert.IsFalse(prop1.IsTouched);
            Assert.IsFalse(prop2.IsTouched);
            Assert.IsFalse(container.IsTouched);
            Assert.IsFalse(prop1.IsDirty);
            Assert.IsFalse(prop2.IsDirty);
            Assert.IsFalse(container.IsDirty);
        }

        [Test]
        public void TestReferenceTypes()
        {
            var object1 = new object();
            var object2 = new object();

            var change1 = new ChangeableProperty<object>(object1);
            Assert.AreEqual(object1, change1.CurrentValue);
            Assert.AreEqual(object1, change1.OriginalValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsFalse(change1.IsTouched);

            change1.CurrentValue = object2;
            Assert.AreEqual(object2, change1.CurrentValue);
            Assert.AreEqual(object1, change1.OriginalValue);
            Assert.IsTrue(change1.IsDirty);
            Assert.IsTrue(change1.IsTouched);

            change1.CurrentValue = object1;
            Assert.AreEqual(object1, change1.CurrentValue);
            Assert.AreEqual(object1, change1.OriginalValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsTrue(change1.IsTouched);
        }

        [Test]
        public void TestTouching()
        {
            var change1 = new ChangeableProperty<bool>(true);
            Assert.IsFalse(change1.IsTouched);

            change1.Touch();
            Assert.IsTrue(change1.IsTouched);

            change1.Touch();
            Assert.IsTrue(change1.IsTouched);

            change1.Reset(ValueResetType.Soft);
            Assert.IsTrue(change1.IsTouched);

            change1.Reset(ValueResetType.Hard);
            Assert.IsFalse(change1.IsTouched);
        }

        [Test]
        public void BasicTest()
        {
            bool eventChangingFired;
            bool eventChangedFired;

            EventHandler<ValueChangingEventArgs> handlerChanging = (sender, args) => { eventChangingFired = true; };
            EventHandler<ValueChangedEventArgs> handlerChanged = (sender, args) => { eventChangedFired = true; };

            eventChangingFired = false;
            eventChangedFired = false;
            var change1 = new ChangeableProperty<bool>(true);
            change1.ValueChanging += handlerChanging;
            change1.ValueChanged += handlerChanged;
            Assert.IsTrue(change1.OriginalValue);
            Assert.IsTrue(change1.CurrentValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsFalse(change1.IsTouched);
            Assert.IsFalse(eventChangingFired);
            Assert.IsFalse(eventChangedFired);

            // Changing to the same value should do nothing
            eventChangingFired = false;
            change1.CurrentValue = true;
            Assert.IsTrue(change1.OriginalValue);
            Assert.IsTrue(change1.CurrentValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsFalse(change1.IsTouched);
            Assert.IsFalse(eventChangingFired);
            Assert.IsFalse(eventChangedFired);

            eventChangingFired = false;
            change1.CurrentValue = false;
            Assert.IsTrue(change1.OriginalValue);
            Assert.IsFalse(change1.CurrentValue);
            Assert.IsTrue(change1.IsDirty);
            Assert.IsTrue(change1.IsTouched);
            Assert.IsTrue(eventChangingFired);

            eventChangingFired = false;
            change1.CurrentValue = true;
            Assert.IsTrue(change1.OriginalValue);
            Assert.IsTrue(change1.CurrentValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsTrue(change1.IsTouched);
            Assert.IsTrue(eventChangingFired);
            Assert.IsTrue(eventChangingFired);
        }

        [Test]
        public void TestCancelling()
        {
            EventHandler<ValueChangingEventArgs> cancelHandler = (sender, args) => { args.Cancel = true; };
            var change1 = new ChangeableProperty<bool>(false);

            // Since our handler cancels the change, there should be no difference to the previous result.
            change1.ValueChanging += cancelHandler;
            change1.CurrentValue = true;
            Assert.IsFalse(change1.OriginalValue);
            Assert.IsFalse(change1.CurrentValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsFalse(change1.IsTouched);

            change1.ValueChanging -= cancelHandler;
            change1.CurrentValue = true;
            Assert.IsFalse(change1.OriginalValue);
            Assert.IsTrue(change1.CurrentValue);
            Assert.IsTrue(change1.IsDirty);
            Assert.IsTrue(change1.IsTouched);
        }

        [Test]
        public void TestReset()
        {
            var change1 = new ChangeableProperty<bool>(false);
            change1.CurrentValue = true;
            Assert.IsFalse(change1.OriginalValue, "Original value must not be changed.");
            Assert.IsTrue(change1.CurrentValue);
            Assert.IsTrue(change1.IsDirty);
            Assert.IsTrue(change1.IsTouched);

            change1.Reset();
            Assert.IsFalse(change1.OriginalValue, "Original value must not be changed by reset methods.");
            Assert.IsFalse(change1.CurrentValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsFalse(change1.IsTouched);

            var change2 = new ChangeableProperty<bool>(false);
            change2.Reset(); // calling reset without changing the value
            Assert.IsFalse(change2.OriginalValue);
            Assert.IsFalse(change2.CurrentValue);
            Assert.IsFalse(change2.IsDirty);
            Assert.IsFalse(change2.IsTouched);

            change2.CurrentValue = true;
            change2.Reset(ValueResetType.Soft);
            Assert.IsFalse(change2.OriginalValue, "Original value must not be changed by reset methods.");
            Assert.IsFalse(change2.CurrentValue, "Current value must be reset to the base value after calling reset.");
            Assert.IsFalse(change2.IsDirty, "IsDirty must be False after calling reset.");
            Assert.IsTrue(change2.IsTouched, "When resetting using soft mode, the IsTouched flag must be not changed.");

            change2.CurrentValue = true;
            change2.Reset(ValueResetType.Hard);
            Assert.IsFalse(change2.OriginalValue, "Original value must not be changed by reset methods.");
            Assert.IsFalse(change2.CurrentValue, "Current value must be reset to the base value after calling reset.");
            Assert.IsFalse(change2.IsDirty, "IsDirty must be False after calling reset.");
            Assert.IsFalse(change2.IsTouched, "When resetting using hard mode, the IsTouched flag must be always false.");
        }

        [Test]
        public void TestCommit()
        {
            var change1 = new ChangeableProperty<bool>(false);
            change1.CurrentValue = true;
            Assert.IsFalse(change1.OriginalValue);
            Assert.IsTrue(change1.CurrentValue);
            Assert.IsTrue(change1.IsDirty);
            Assert.IsTrue(change1.IsTouched);

            change1.Commit();
            Assert.IsTrue(change1.OriginalValue);
            Assert.IsTrue(change1.CurrentValue);
            Assert.IsFalse(change1.IsDirty);
            Assert.IsFalse(change1.IsTouched);
        }
    }
}

