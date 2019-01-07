using System;
using System.Collections.Generic;
using CSharp_SpotifyAPI.Enums;
using CSharp_SpotifyAPI;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using CSharp_SpotifyAPI.Models;

namespace CSharp_SpotifyAPITests
{
    /// <summary>
    /// Test class for <see cref="SpotifyAPI"/>.
    /// Naming convention follows  [UnitOfWork_StateUnderTest_ExpectedBehavior].
    /// </summary>
    [TestClass]
    public class SpotifyAPITests
    {
        /// <summary>
        /// Manual reset event to halt the <see cref="Setup"/> method and wait for 
        /// Spotify authentication to finish.
        /// </summary>
        ManualResetEvent spotifyAuthenticationCompleted;

        /// <summary>
        /// Sets up and authenticates the Spotify API for testing.
        /// </summary>
        [TestMethod]
        public void Setup()
        {
            Spotify spotifyApi = new Spotify();

            Spotify.Api.Authenticated += API_Authenticated;

            spotifyAuthenticationCompleted = new ManualResetEvent(false);

            spotifyApi.Authenticate();

            // Wait until the API has authenticated.
            spotifyAuthenticationCompleted.WaitOne();
        }

        private void API_Authenticated(object sender, EventArgs e)
        {
            // Set the ManualResetEvent to allow the Setup method to continue.
            spotifyAuthenticationCompleted.Set();
        }

        #region AlbumsTests

        #region GetAlbum

        [TestMethod]
        public void GetAlbum_ValidAlbumId_DeserialisesCorrectly()
        {
            // Arrange
            string validAlbumId = "0sNOF9WDwhWunNAHPD3Baj";

            // Act
            Album album = JsonConvert.DeserializeObject<Album>(Spotify.Api.GetAlbum(validAlbumId));

            // Assert
            Assert.IsNotNull(album);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAlbum_InvalidAlbumId_ThrowsException()
        {
            // Arrange
            string invalidAlbumId = "1231235234442224423";

            // Act
            Spotify.Api.GetAlbum(invalidAlbumId);

            // Assert is handled by the ExpectedException attribute on the test method.
        }

        [TestMethod]
        public void GetAlbumInSpecificMarket_ValidAlbumId_DeserialisesCorrectly()
        {
            // Arrange
            string validAlbumId = "4aawyAB9vmqN3uQ7FjRGTy";

            // Act
            Album album = JsonConvert.DeserializeObject<Album>(Spotify.Api.GetAlbum(validAlbumId, Market.AU));

            // Assert
            Assert.IsNotNull(album);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAlbumInSpecificMarket_InvalidValidAlbumId_ThrowsException()
        {
            // Arrange
            string invalidAlbumId = "1231235234442224423";

            // Act
            Spotify.Api.GetAlbum(invalidAlbumId, Market.AU);

            // Assert is handled by the ExpectedException attribute on the test method.
        }

        #endregion

        #region GetSeveralAlbums

        [TestMethod]
        public void GetSeveralAlbums_ValidAlbumIds_DeserialisesCorrectly()
        {
            // Arrange
            List<string> albumsList = new List<string>()
            {
                "4aawyAB9vmqN3uQ7FjRGTy",
                "0sNOF9WDwhWunNAHPD3Baj"
            };

            // Act
            Albums albums = JsonConvert.DeserializeObject<Albums>(Spotify.Api.GetSeveralAlbums(albumsList));

            // Assert
            Assert.IsNotNull(albums.AlbumsList);
        }

        [TestMethod]
        public void GetSeveralAlbums_InvalidAlbumIds_AlbumItemsAreNull()
        {
            // Arrange
            List<string> albumsList = new List<string>()
            {
                "5343453453453453123",
                "12312534636456242342"
            };

            // Act
            Albums albums = JsonConvert.DeserializeObject<Albums>(Spotify.Api.GetSeveralAlbums(albumsList));

            // Assert

            // Assert the Albums object is not null.
            Assert.IsNotNull(albums);

            // Assert each item of the AlbumsList is null.
            foreach (var album in albums.AlbumsList)
            {
                Assert.IsNull(album);
            }
        }

        [TestMethod]
        public void GetSeveralAlbumsInSpecificMarket_ValidAlbumIds_DeserialisesCorrectly()
        {
            // Arrange
            List<string> albumsList = new List<string>()
            {
                "4aawyAB9vmqN3uQ7FjRGTy",
                "0sNOF9WDwhWunNAHPD3Baj"
            };

            // Act
            Albums albums = JsonConvert.DeserializeObject<Albums>(Spotify.Api.GetSeveralAlbums(albumsList, Market.AU));

            // Assert
            Assert.IsNotNull(albums.AlbumsList);
        }

        [TestMethod]
        public void GetSeveralAlbumsInSpecificMarket_InvalidAlbumIds_AlbumItemsAreNull()
        {
            // Arrange
            List<string> albumsList = new List<string>()
            {
                "5343453453453453123",
                "12312534636456242342"
            };

            // Act
            Albums albums = JsonConvert.DeserializeObject<Albums>(Spotify.Api.GetSeveralAlbums(albumsList, Market.AU));

            // Assert

            // Assert the Albums object is not null.
            Assert.IsNotNull(albums);

            // Assert each item of the AlbumsList is null.
            foreach (var album in albums.AlbumsList)
            {
                Assert.IsNull(album);
            }
        }

        #endregion

        #region GetAlbumTracks

        [TestMethod]
        public void GetAlbumTracks_ValidAlbumId_DeserialisesCorrectly()
        {
            // Arranage
            string albumId = "4aawyAB9vmqN3uQ7FjRGTy";
            int limit = 10;
            int offset = 0;

            // Act
            AlbumTrackPagingObject tracks = JsonConvert.DeserializeObject<AlbumTrackPagingObject>(Spotify.Api.GetAlbumTracks(albumId, limit, offset));

            // Assert
            Assert.IsNotNull(tracks.Items);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAlbumTracks_InvalidAlbumId_ThrowsException()
        {
            // Arranage
            string albumId = "123123123123123";
            int limit = 10;
            int offset = 0;

            // Act
            AlbumTrackPagingObject tracks = JsonConvert.DeserializeObject<AlbumTrackPagingObject>(Spotify.Api.GetAlbumTracks(albumId, limit, offset));

            // Assert is handled by the ExpectedException attribute on the test method.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAlbumTracks_InvalidLimit_ThrowsException()
        {
            // Arranage
            string albumId = "4aawyAB9vmqN3uQ7FjRGTy";
            int limit = -10;
            int offset = 0;

            // Act
            AlbumTrackPagingObject tracks = JsonConvert.DeserializeObject<AlbumTrackPagingObject>(Spotify.Api.GetAlbumTracks(albumId, limit, offset));

            // Assert is handled by the ExpectedException attribute on the test method.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAlbumTracks_InvalidOffset_ThrowsException()
        {
            // Arranage
            string albumId = "4aawyAB9vmqN3uQ7FjRGTy";
            int limit = 10;
            int offset = -10;

            // Act
            AlbumTrackPagingObject tracks = JsonConvert.DeserializeObject<AlbumTrackPagingObject>(Spotify.Api.GetAlbumTracks(albumId, limit, offset));

            // Assert is handled by the ExpectedException attribute on the test method.
        }

        #endregion

        #endregion

        #region ArtistsTests

        [TestMethod]
        public void GetArtist_ValidArtistId_DeserialisesCorrectly()
        {
            // Arrange
            string validArtistID = "0OdUWJ0sBjDrqHygGUXeCF";

            // Act
            Artist artist = JsonConvert.DeserializeObject<Artist>(Spotify.Api.GetArtist(validArtistID));

            // Assert
            Assert.IsNotNull(artist);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetArtist_InvalidArtistId_DeserialisesCorrectly()
        {
            // Arrange
            string validArtistID = "123123534534123";

            // Act
            Artist artist = JsonConvert.DeserializeObject<Artist>(Spotify.Api.GetArtist(validArtistID));

            // Assert is handled by the ExpectedException attribute on the test method.
        }

        [TestMethod]
        public void GetSeveralArtists_ValidArtistIds_DeserialisesCorrectly()
        {

        }

        #endregion

    }
}
