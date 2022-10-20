using AutoMapper;
using FamilyTree_BAL.Interface;
using FamilyTree.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FamilyTree.Tests;

public class DescriptionControllerTests
{
    private readonly Mock<IServiceHub> mockHub;
    private readonly DescriptionController controller;

    public DescriptionControllerTests()
    {
        mockHub = new Mock<IServiceHub>();
        controller = new DescriptionController(mockHub.Object);
    }
    
    [Theory]
    [InlineData(1, null)]
    public void ChangeInformation_ActionExecutes_ReturnsViewForChangeInformation(int personId, PersonVM person)
    {
        var result = controller.ChangeInformation(personId, person);
        Assert.IsType<NoContentResult>(result);
    }
}