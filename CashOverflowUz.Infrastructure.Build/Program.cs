using System.Collections.Generic;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var githubPiplene = new GithubPipeline
{
    Name = "CashOverflowUz",

    OnEvents = new Events
    {
        Push = new PushEvent
        {
            Branches = new string[] { "Master" }
        },

        PullRequest = new PullRequestEvent
        {
            Branches = new string[] { "Master" }
        }
    },

    Jobs = new Jobs
    {
        Build = new BuildJob
        {
            RunsOn = BuildMachines.UbuntuLatest,

            Steps = new List<GithubTask>
              {
                new CheckoutTaskV2
                {
                     Name = "checking out"
                },
                new SetupDotNetTaskV1
                {
                     Name = "Instaling .Net",

                    TargetDotNetVersion = new TargetDotNetVersion
                      {
                           DotNetVersion = "7.0.200"
                      }
                },

                new RestoreTask
                {
                     Name ="Restoring packages..."
                },
                new DotNetBuildTask
                {
                     Name = "Building project"
                },
                new TestTask
                {
                     Name = "Testing project"
                }

            }
        }
    }
};

var adotnetclint = new ADotNetClient();
adotnetclint.SerializeAndWriteToFile(
    githubPiplene,
    path: ("../../../../.github/workflows/build.yml"));