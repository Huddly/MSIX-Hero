﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using otor.msixhero.lib.BusinessLayer.Appx.Packer;
using otor.msixhero.lib.BusinessLayer.Appx.Signing;
using otor.msixhero.lib.Domain.Appx.Signing;
using otor.msixhero.lib.Infrastructure;
using otor.msixhero.lib.Infrastructure.Configuration;
using otor.msixhero.lib.Infrastructure.Progress;
using otor.msixhero.ui.Domain;
using otor.msixhero.ui.Modules.Dialogs.Pack.ViewModel;

namespace otor.msixhero.lib.tests.ViewModels
{
    [TestFixture]
    public class PackViewModelTests
    {
        [Test]
        public void Test()
        {
            var mockPacker = new Mock<IAppxPacker>();
            var mockInteraction = new Mock<IInteractionService>();
            var mockConfiguration = new Mock<IConfigurationService>();
            var mockSigning = new Mock<IAppxSigningManager>();

            var config = new Configuration();
            config.Packer.DefaultOutFolder = @"c:\temp";

            mockConfiguration.Setup(dep => dep.GetCurrentConfiguration(false)).Returns(config);
            mockConfiguration.Setup(dep => dep.GetCurrentConfiguration(true)).Returns(config);
            mockSigning.Setup(dep => dep.GetCertificatesFromStore(It.IsAny<CertificateStoreType>(), It.IsAny<bool>(),
                    It.IsAny<CancellationToken>(), It.IsAny<IProgress<ProgressData>>()))
                .ReturnsAsync(new List<PersonalCertificate>());

            var viewModel = new PackViewModel(mockPacker.Object, mockSigning.Object, mockConfiguration.Object, mockInteraction.Object);

            Assert.AreEqual(ValidationMode.Silent, viewModel.ValidationMode);
            Assert.IsFalse(viewModel.HasError);
            Assert.IsFalse(viewModel.IsValid);

            viewModel.Save(false);
            Assert.AreEqual(ValidationMode.Default, viewModel.ValidationMode);
            Assert.IsTrue(viewModel.HasError);

            Assert.IsTrue(string.IsNullOrEmpty(viewModel.OutputPath.CurrentValue));
            viewModel.InputPath.CurrentValue = @"c:\myFolder\temp123";
            Assert.AreEqual(@"c:\myFolder\temp123\_packed\temp123.msix", viewModel.OutputPath.CurrentValue);
        }
    }
}
