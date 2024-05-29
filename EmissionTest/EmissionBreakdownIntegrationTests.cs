using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using EmissionBreakdownApi;
using EmissionBreakdownApi.DTOs;

public class EmissionBreakdownIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EmissionBreakdownIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    // This test is incomplete.
    #region QueryTest
    [Fact]
    public async Task Query_ValidParameters_ReturnsOk()
    {
        // Arrange
        var validParameters = new EmissionBreakdownQueryParametersDTO
        {
            // Set valid query parameters here
        };

        // Act
        var response = await _client.GetAsync($"/EmissionBreakdown?{validParameters}");

        // Assert
        response.EnsureSuccessStatusCode();
        var results = await response.Content.ReadFromJsonAsync<EmissionBreakdownQueryParametersDTO>();
        Xunit.Assert.NotNull(results);
       // Xunit.Assert.Equal(/* Add assertions for expected results */);
    }

    [Fact]
    public async Task Query_InvalidParameters_ReturnsBadRequest()
    {
        // Arrange
        var invalidParameters = new EmissionBreakdownQueryParametersDTO
        {
            // Set invalid query parameters here
        };

        // Act
        var response = await _client.GetAsync($"/EmissionBreakdown?{invalidParameters}");

        // Assert
        Xunit.Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
    #endregion

    #region GetTest
    [Fact]
    public async Task Get_ExistingId_ReturnsOk()
    {
        // Arrange
        var existingId = 1; // Replace with an ID that exists in your test database

        // Act
        var response = await _client.GetAsync($"/EmissionBreakdown/{existingId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var emissionBreakdown = await response.Content.ReadFromJsonAsync<EmissionBreakdownRowDTO>();
        Xunit.Assert.NotNull(emissionBreakdown);
        Xunit.Assert.Equal(existingId, emissionBreakdown.Id);
    }

    [Fact]
    public async Task Get_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = 999; // Replace with an ID that does not exist in your test database

        // Act
        var response = await _client.GetAsync($"/EmissionBreakdown/{nonExistingId}");

        // Assert
        Xunit.Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    #endregion

    #region CreateTest
    [Fact]
    public async Task Create_ValidData_ReturnsCreated()
    {
        // Arrange
        var newRow = new EmissionBreakdownRowDTO
        {
            Id = 6,
            // This ID will be set by the database, so it can be left as default or 0.
            // Initialize the Category and SubCategory with valid data.
            Category = new EmissionCategoryDTO
            {
                Id = "1", // Replace with a valid category ID from your test database
                Name = "Category 1"
            },
            SubCategory = new EmissionSubCategoryDTO
            {
                Id = "1", // Replace with a valid subcategory ID from your test database
                Name = "SubCategory 1"
            },
            TonsOfCO2 = 123.45
        };

        // Act
        var response = await _client.PostAsJsonAsync("/EmissionBreakdown", newRow);

        // Assert
        response.EnsureSuccessStatusCode();
        Xunit.Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        var createdRow = await response.Content.ReadFromJsonAsync<CreatedEmissionBreakdownRowDTO>();
        Xunit.Assert.NotNull(createdRow);
        //Xunit.Assert.NotEqual(0, createdRow.RowId);
        Xunit.Assert.Equal(newRow.Category.Name, createdRow.Data.Category.Name);
        Xunit.Assert.Equal(newRow.SubCategory.Name, createdRow.Data.SubCategory.Name);
        Xunit.Assert.Equal(newRow.TonsOfCO2, createdRow.Data.TonsOfCO2);
    }

    [Fact]
    public async Task Create_InvalidData_ReturnsBadRequest()
    {
        // Arrange
        var invalidRow = new EmissionBreakdownRowDTO
        {
            Id = 6,
            Category = new EmissionCategoryDTO
            {
                Id = "",
                Name = "Category 1"
            },
            SubCategory = new EmissionSubCategoryDTO
            {
                Id = "",
                Name = "SubCategory 1"
            },
            TonsOfCO2 = 123.45
        };

        // Act
        var response = await _client.PostAsJsonAsync("/EmissionBreakdown", invalidRow);

        // Assert
        Xunit.Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
    #endregion

    #region DeleteTest
    [Fact]
    public async Task Delete_ExistingId_ReturnsOk()
    {
        // Arrange
        var existingId = 1;

        // Act
        var response = await _client.DeleteAsync($"/EmissionBreakdown/{existingId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Xunit.Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Delete_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = 999;

        // Act
        var response = await _client.DeleteAsync($"/EmissionBreakdown/{nonExistingId}");

        // Assert
        Xunit.Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    #endregion

    #region UpdateTest
    [Fact]
    public async Task Update_ExistingId_ReturnsOk()
    {
        // Arrange
        var existingId = 1;
        var updatedRow = new EmissionBreakdownRowDTO
        {
            Id = existingId,
            Category = new EmissionCategoryDTO
            {
                Id = "1",
                Name = ""
            },
            SubCategory = new EmissionSubCategoryDTO
            {
                Id = "2",
                Name = ""
            },
            TonsOfCO2 = 456.78
        };

        // Act
        var response = await _client.PatchAsJsonAsync($"/EmissionBreakdown/{existingId}", updatedRow);

        // Assert
        response.EnsureSuccessStatusCode();
        Xunit.Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        var returnedRow = await response.Content.ReadFromJsonAsync<EmissionBreakdownRowDTO>();
        Xunit.Assert.NotNull(returnedRow);
        Xunit.Assert.Equal(updatedRow.Category.Name, returnedRow.Category.Name);
        Xunit.Assert.Equal(updatedRow.SubCategory.Name, returnedRow.SubCategory.Name);
        Xunit.Assert.Equal(updatedRow.TonsOfCO2, returnedRow.TonsOfCO2);
    }

    [Fact]
    public async Task Update_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = 999;
        var updatedRow = new EmissionBreakdownRowDTO
        {
            Id = nonExistingId,
            Category = new EmissionCategoryDTO
            {
                Id = "999",
                Name = ""
            },
            SubCategory = new EmissionSubCategoryDTO
            {
                Id = "123",
                Name = ""
            },
            TonsOfCO2 = 456.78
        };

        // Act
        var response = await _client.PatchAsJsonAsync($"/EmissionBreakdown/{nonExistingId}", updatedRow);

        // Assert
        Xunit.Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    #endregion
}