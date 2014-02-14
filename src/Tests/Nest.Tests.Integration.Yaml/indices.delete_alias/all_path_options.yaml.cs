using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesDeleteAlias2
{
	public partial class IndicesDeleteAlias2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index1", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index2", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("foo", null));

				//do indices.put_alias 
				_body = new {
					routing= "routing value"
				};
				this.Do(()=> this._client.IndicesPutAliasPost("alias1", _body));

				//do indices.put_alias 
				_body = new {
					routing= "routing value"
				};
				this.Do(()=> this._client.IndicesPutAliasPost("alias2", _body));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckSetup2Tests : YamlTestsBase
		{
			[Test]
			public void CheckSetup2Test()
			{	

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"));

				//match _response.test_index1.aliases.alias1.search_routing: 
				this.IsMatch(_response.test_index1.aliases.alias1.search_routing, @"routing value");

				//match _response.test_index2.aliases.alias1.search_routing: 
				this.IsMatch(_response.test_index2.aliases.alias1.search_routing, @"routing value");

				//match _response.foo.aliases.alias1.search_routing: 
				this.IsMatch(_response.foo.aliases.alias1.search_routing, @"routing value");

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.test_index1.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index1.aliases.alias2.search_routing, @"routing value");

				//match _response.test_index2.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index2.aliases.alias2.search_routing, @"routing value");

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithAllIndex3Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithAllIndex3Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("_all", "alias1"));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"), shouldCatch: @"missing");

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.test_index1.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index1.aliases.alias2.search_routing, @"routing value");

				//match _response.test_index2.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index2.aliases.alias2.search_routing, @"routing value");

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndex4Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithIndex4Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("*", "alias1"));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"), shouldCatch: @"missing");

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.test_index1.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index1.aliases.alias2.search_routing, @"routing value");

				//match _response.test_index2.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index2.aliases.alias2.search_routing, @"routing value");

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexList5Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithIndexList5Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("test_index1,test_index2", "alias1"));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"));

				//match _response.foo.aliases.alias1.search_routing: 
				this.IsMatch(_response.foo.aliases.alias1.search_routing, @"routing value");

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.test_index1.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index1.aliases.alias2.search_routing, @"routing value");

				//match _response.test_index2.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index2.aliases.alias2.search_routing, @"routing value");

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithPrefixIndex6Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithPrefixIndex6Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("test_*", "alias1"));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"));

				//match _response.foo.aliases.alias1.search_routing: 
				this.IsMatch(_response.foo.aliases.alias1.search_routing, @"routing value");

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.test_index1.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index1.aliases.alias2.search_routing, @"routing value");

				//match _response.test_index2.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index2.aliases.alias2.search_routing, @"routing value");

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexListAndAliases7Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithIndexListAndAliases7Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("test_index1,test_index2", "*"));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"));

				//match _response.foo.aliases.alias1.search_routing: 
				this.IsMatch(_response.foo.aliases.alias1.search_routing, @"routing value");

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexListAndAllAliases8Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithIndexListAndAllAliases8Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("test_index1,test_index2", "_all"));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"));

				//match _response.foo.aliases.alias1.search_routing: 
				this.IsMatch(_response.foo.aliases.alias1.search_routing, @"routing value");

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexListAndWildcardAliases9Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithIndexListAndWildcardAliases9Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("test_index1,test_index2", "*1"));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias1"));

				//match _response.foo.aliases.alias1.search_routing: 
				this.IsMatch(_response.foo.aliases.alias1.search_routing, @"routing value");

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias2"));

				//match _response.test_index1.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index1.aliases.alias2.search_routing, @"routing value");

				//match _response.test_index2.aliases.alias2.search_routing: 
				this.IsMatch(_response.test_index2.aliases.alias2.search_routing, @"routing value");

				//match _response.foo.aliases.alias2.search_routing: 
				this.IsMatch(_response.foo.aliases.alias2.search_routing, @"routing value");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Check404OnNoMatchingAlias10Tests : YamlTestsBase
		{
			[Test]
			public void Check404OnNoMatchingAlias10Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("*", "non_existent"), shouldCatch: @"missing");

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("non_existent", "alias1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithBlankIndexAndBlankAlias11Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithBlankIndexAndBlankAlias11Test()
			{	

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("alias1"), shouldCatch: @"param");

				//do indices.delete_alias 
				this.Do(()=> this._client.IndicesDeleteAlias("test_index1"), shouldCatch: @"param");

			}
		}
	}
}
