/* Created by and for usage of FF Studios (2021). */

using System.Linq;
using UnityEngine;
using UnityEditor;

namespace FFEditor
{
	public class FFModelImportUtility : AssetPostprocessor
	{
		public Material defaultMaterial;

		static readonly string prefix_prop = "prop";
		static readonly string prefix_envr = "envr";
		static readonly string prefix_gnrc = "gnrc";
		static readonly string prefix_char = "char";
		static readonly string prefix_anim = "anim";

		void OnPreprocessModel()
		{
			var modelPrefix = GetModelPrefix();
			
			if( ShouldProcess( modelPrefix ) == false )
				return;

			var modelImporter = assetImporter as ModelImporter;

			/* Model Tab. */
			modelImporter.importBlendShapes = false;
			modelImporter.importVisibility = false;
			modelImporter.importCameras = false;
			modelImporter.importLights = false;

			/* Rig Tab. */
			if( modelPrefix == prefix_prop || modelPrefix == prefix_envr )
				modelImporter.animationType = ModelImporterAnimationType.None;
            else if( modelPrefix == prefix_char )
				modelImporter.animationType = ModelImporterAnimationType.Human;
            else if( modelPrefix == prefix_gnrc )
				modelImporter.animationType = ModelImporterAnimationType.Generic;

			/* Animation Tab. */
            modelImporter.importAnimation = modelPrefix == prefix_char || modelPrefix == prefix_anim ;

			AssetDatabase.ImportAsset( assetPath );
		}

		void OnPostprocessModel( GameObject gameObject )
		{
			if( ShouldProcess( GetModelPrefix() ) )
			{
				FFStudio.FFLogger.Log( "FFModelImportUtility: Remapping default material for " + assetPath.Split( '/' ).Last() + ".",
								   	   gameObject );
				RemapDefaultMaterial( gameObject.transform );
				AssetDatabase.WriteImportSettingsIfDirty( assetPath );
			}
		}

		void RemapDefaultMaterial( Transform transform )
		{
			Renderer renderer = transform.gameObject.GetComponent< Renderer >();

			if( defaultMaterial == null )
				defaultMaterial = AssetDatabase.LoadAssetAtPath< Material >( "Assets/Material/mat_atlas.mat" );

			if( renderer != null )
			{
				Material[] materials = renderer.sharedMaterials;
                
				foreach( var material in materials )
					assetImporter.AddRemap( new AssetImporter.SourceAssetIdentifier( material ), defaultMaterial );
			}

			// Recurse.
			foreach( Transform child in transform )
				RemapDefaultMaterial( child );
		}

		bool ShouldProcess( string prefix )
		{
			var modelImporter = assetImporter as ModelImporter;

			// Info: Only pre-process models for the FIRST time.
			if( modelImporter.importSettingsMissing == false )
				return false;

			return prefix == prefix_prop || prefix == prefix_envr || prefix == prefix_gnrc || prefix == prefix_char;
		}
		
		string GetModelPrefix()
		{
			var modelNameOnly = assetPath.Split( '/' ).Last();
			var modelPrefix = modelNameOnly.Split( '_' ).First();
			return modelPrefix;
		}
	}
}