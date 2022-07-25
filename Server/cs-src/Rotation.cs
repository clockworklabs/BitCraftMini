// THIS FILE IS AUTOMATICALLY GENERATED BY SPACETIMEDB. EDITS TO THIS FILE
// WILL NOT BE SAVED. MODIFY TABLES IN RUST INSTEAD.

namespace SpacetimeDB
{
	public partial class Rotation
	{
		[Newtonsoft.Json.JsonProperty("rot_x")]
		public float rotX;
		[Newtonsoft.Json.JsonProperty("rot_y")]
		public float rotY;
		[Newtonsoft.Json.JsonProperty("rot_z")]
		public float rotZ;
		[Newtonsoft.Json.JsonProperty("rot_w")]
		public float rotW;
		public static TypeDef GetTypeDef()
		{
			return TypeDef.Tuple(new ElementDef[]
			{
				new SpacetimeDB.ElementDef(0, SpacetimeDB.TypeDef.BuiltInType(SpacetimeDB.TypeDef.Def.F32)),
				new SpacetimeDB.ElementDef(1, SpacetimeDB.TypeDef.BuiltInType(SpacetimeDB.TypeDef.Def.F32)),
				new SpacetimeDB.ElementDef(2, SpacetimeDB.TypeDef.BuiltInType(SpacetimeDB.TypeDef.Def.F32)),
				new SpacetimeDB.ElementDef(3, SpacetimeDB.TypeDef.BuiltInType(SpacetimeDB.TypeDef.Def.F32)),
			});
		}
		public static Rotation From(TypeValue value)
		{
			var tupleValue = value.GetValue(TypeDef.Def.Tuple) as TypeValue[];
			if (tupleValue == null)
			{
				throw new System.InvalidOperationException("Invalid value (must be Tuple): {value.GetType()}");
			}
			return new Rotation
			{
				rotX = (float)tupleValue[0].GetValue(TypeDef.Def.F32),
				rotY = (float)tupleValue[1].GetValue(TypeDef.Def.F32),
				rotZ = (float)tupleValue[2].GetValue(TypeDef.Def.F32),
				rotW = (float)tupleValue[3].GetValue(TypeDef.Def.F32),
			};
		}
	}
}
