// THIS FILE IS AUTOMATICALLY GENERATED BY SPACETIMEDB. EDITS TO THIS FILE
// WILL NOT BE SAVED. MODIFY TABLES IN RUST INSTEAD.

using System;

namespace SpacetimeDB
{
	public partial class Deposit : IDatabaseTable
	{
		[Newtonsoft.Json.JsonProperty("entity_id")]
		public ulong EntityId;
		[Newtonsoft.Json.JsonProperty("chunk")]
		public SpacetimeDB.ChunkPosition Chunk;
		[Newtonsoft.Json.JsonProperty("deposit_idx")]
		public ushort DepositIdx;
		[Newtonsoft.Json.JsonProperty("x")]
		public float X;
		[Newtonsoft.Json.JsonProperty("y")]
		public float Y;
		[Newtonsoft.Json.JsonProperty("scale")]
		public float Scale;

		public static SpacetimeDB.SATS.AlgebraicType GetAlgebraicType()
		{
			return SpacetimeDB.SATS.AlgebraicType.CreateProductType(new SpacetimeDB.SATS.ProductTypeElement[]
			{
				new SpacetimeDB.SATS.ProductTypeElement("entity_id", SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.U64)),
				new SpacetimeDB.SATS.ProductTypeElement("chunk", SpacetimeDB.ChunkPosition.GetAlgebraicType()),
				new SpacetimeDB.SATS.ProductTypeElement("deposit_idx", SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.U16)),
				new SpacetimeDB.SATS.ProductTypeElement("x", SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.F32)),
				new SpacetimeDB.SATS.ProductTypeElement("y", SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.F32)),
				new SpacetimeDB.SATS.ProductTypeElement("scale", SpacetimeDB.SATS.AlgebraicType.CreatePrimitiveType(SpacetimeDB.SATS.BuiltinType.Type.F32)),
			});
		}

		public static explicit operator Deposit(SpacetimeDB.SATS.AlgebraicValue value)
		{
			if (value == null) {
				return null;
			}
			var productValue = value.AsProductValue();
			return new Deposit
			{
				EntityId = productValue.elements[0].AsU64(),
				Chunk = (SpacetimeDB.ChunkPosition)(productValue.elements[1]),
				DepositIdx = productValue.elements[2].AsU16(),
				X = productValue.elements[3].AsF32(),
				Y = productValue.elements[4].AsF32(),
				Scale = productValue.elements[5].AsF32(),
			};
		}

	}
}
