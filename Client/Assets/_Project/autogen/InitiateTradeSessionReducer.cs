// THIS FILE IS AUTOMATICALLY GENERATED BY SPACETIMEDB. EDITS TO THIS FILE
// WILL NOT BE SAVED. MODIFY TABLES IN RUST INSTEAD.

using System;
using ClientApi;
using Newtonsoft.Json.Linq;

namespace SpacetimeDB
{
	public static partial class Reducer
	{
		public static event Action<ClientApi.Event.Types.Status, Identity, ulong, ulong> OnInitiateTradeSessionEvent;

		public static void InitiateTradeSession(ulong initiatorEntityId, ulong acceptorEntityId)
		{
			var _argArray = new object[] {initiatorEntityId, acceptorEntityId};
			var _message = new NetworkManager.ReducerCallRequest {
				fn = "initiate_trade_session",
				args = _argArray,
			};
			Newtonsoft.Json.JsonSerializerSettings _settings = new Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = { new SpacetimeDB.SomeWrapperConverter(), new SpacetimeDB.EnumWrapperConverter() },
				ContractResolver = new SpacetimeDB.JsonContractResolver(),
			};
			NetworkManager.instance.InternalCallReducer(Newtonsoft.Json.JsonConvert.SerializeObject(_message, _settings));
		}

		[ReducerEvent(FunctionName = "initiate_trade_session")]
		public static void OnInitiateTradeSession(ClientApi.Event dbEvent)
		{
			if(OnInitiateTradeSessionEvent != null)
			{
				var bsatnBytes = dbEvent.FunctionCall.ArgBytes;
				using var ms = new System.IO.MemoryStream();
				ms.SetLength(bsatnBytes.Length);
				bsatnBytes.CopyTo(ms.GetBuffer(), 0);
				ms.Position = 0;
				using var reader = new System.IO.BinaryReader(ms);
				var args_0_value = SpacetimeDB.SATS.AlgebraicValue.Deserialize(SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.U64), reader);
				var args_0 = args_0_value.AsU64();
				var args_1_value = SpacetimeDB.SATS.AlgebraicValue.Deserialize(SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.U64), reader);
				var args_1 = args_1_value.AsU64();
				OnInitiateTradeSessionEvent(dbEvent.Status, Identity.From(dbEvent.CallerIdentity.ToByteArray()), args_0, args_1);
			}
		}
		[DeserializeEvent(FunctionName = "initiate_trade_session")]
		public static object[] InitiateTradeSessionDeserializeEventArgs(ClientApi.Event dbEvent)
		{
			var bsatnBytes = dbEvent.FunctionCall.ArgBytes;
			using var ms = new System.IO.MemoryStream();
			ms.SetLength(bsatnBytes.Length);
			bsatnBytes.CopyTo(ms.GetBuffer(), 0);
			ms.Position = 0;
			using var reader = new System.IO.BinaryReader(ms);
			var args_0_value = SpacetimeDB.SATS.AlgebraicValue.Deserialize(SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.U64), reader);
			var args_0 = args_0_value.AsU64();
			var args_1_value = SpacetimeDB.SATS.AlgebraicValue.Deserialize(SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.U64), reader);
			var args_1 = args_1_value.AsU64();
			return new object[] {
				args_0,
				args_1,
			};
		}
	}
}
