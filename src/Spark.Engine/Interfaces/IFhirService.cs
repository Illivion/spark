using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Spark.Engine.Core;

namespace Spark.Service
{
    public interface IFhirService
    {
        FhirResponse Read(Key key);
        FhirResponse ReadMeta(Key key);
        FhirResponse AddMeta(Key key, Parameters parameters);

        /// <summary>
        /// Read the state of a specific version of the resource.
        /// </summary>
        /// <param name="collectionName">The resource type, in lowercase</param>
        /// <param name="id">The id part of a version-specific reference</param>
        /// <param name="vid">The version part of a version-specific reference</param>
        /// <returns>A Result containing the resource, or an Issue</returns>
        /// <remarks>
        /// If the version referred to is actually one where the resource was deleted, the server should return a 
        /// 410 status code. 
        /// </remarks>
        FhirResponse VersionRead(Key key);

        /// <summary>
        /// Create a new resource with a server assigned id.
        /// </summary>
        /// <param name="collection">The resource type, in lowercase</param>
        /// <param name="resource">The data for the Resource to be created</param>
        /// <returns>
        /// Returns 
        ///     201 Created - on successful creation
        /// </returns>
        FhirResponse Create(IKey key, Resource resource);

        FhirResponse Put(IKey key, Resource resource);
        FhirResponse ConditionalCreate(IKey key, Resource resource, IEnumerable<Tuple<string, string>> query);
        FhirResponse Search(string type, SearchParams searchCommand);
        FhirResponse VersionSpecificUpdate(IKey versionedkey, Resource resource);

        /// <summary>
        /// Updates a resource if it exist on the given id, or creates the resource if it is new.
        /// If a VersionId is included a version specific update will be attempted.
        /// </summary>
        /// <returns>200 OK (on success)</returns>
        FhirResponse Update(IKey key, Resource resource);

        FhirResponse ConditionalUpdate(Key key, Resource resource, SearchParams _params);

        /// <summary>
        /// Delete a resource.
        /// </summary>
        /// <param name="collection">The resource type, in lowercase</param>
        /// <param name="id">The id part of a Resource id</param>
        /// <remarks>
        /// Upon successful deletion the server should return 
        ///   * 204 (No Content). 
        ///   * If the resource does not exist on the server, the server must return 404 (Not found).
        ///   * Performing this operation on a resource that is already deleted has no effect, and should return 204 (No Content).
        /// </remarks>
        FhirResponse Delete(IKey key);

        FhirResponse ConditionalDelete(Key key, IEnumerable<Tuple<string, string>> parameters);
        FhirResponse HandleInteraction(Interaction interaction);
        FhirResponse Transaction(IList<Interaction> interactions);
        FhirResponse Transaction(Bundle bundle);
        FhirResponse History(HistoryParameters parameters);
        FhirResponse History(string type, HistoryParameters parameters);
        FhirResponse History(Key key, HistoryParameters parameters);
        FhirResponse Mailbox(Bundle bundle, Binary body);
        FhirResponse ValidateOperation(Key key, Resource resource);
        FhirResponse Conformance();
        FhirResponse GetPage(string snapshotkey, int index, int count);
    }
}